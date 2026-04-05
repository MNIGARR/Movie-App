using MovieApp.API.DTOs;
using MovieApp.API.Models;
using MovieApp.API.Repositories.Interfaces;

namespace MovieApp.API.Services;

public class WatchlistService
{
    private readonly IWatchlistRepository _watchlistRepository;
    private readonly IMovieRepository _movieRepository;

    public WatchlistService(IWatchlistRepository watchlistRepository, IMovieRepository movieRepository)
    {
        _watchlistRepository = watchlistRepository;
        _movieRepository = movieRepository;
    }

    public async Task<IEnumerable<WatchlistDto>> GetUserWatchlistAsync(int userId)
    {
        var items = await _watchlistRepository.GetUserWatchlistAsync(userId);
        return items.Select(w => new WatchlistDto
        {
            Id = w.Id,
            MovieId = w.MovieId,
            MovieTitle = w.Movie.Title,
            MoviePosterPath = w.Movie.PosterPath,
            CreatedAt = w.CreatedAt
        });
    }

    public async Task<(bool Success, string Message, WatchlistDto? Item)> AddAsync(int userId, int movieId)
    {
        if (!await _movieRepository.ExistsAsync(movieId))
            return (false, "Movie not found.", null);

        var existing = await _watchlistRepository.GetAsync(userId, movieId);
        if (existing != null)
            return (false, "Movie already in watchlist.", null);

        var watchlist = new Watchlist { UserId = userId, MovieId = movieId };
        var created = await _watchlistRepository.AddAsync(watchlist);

        var movie = await _movieRepository.GetByIdAsync(movieId);
        var dto = new WatchlistDto
        {
            Id = created.Id,
            MovieId = created.MovieId,
            MovieTitle = movie!.Title,
            MoviePosterPath = movie.PosterPath,
            CreatedAt = created.CreatedAt
        };

        return (true, "Movie added to watchlist.", dto);
    }

    public async Task<(bool Success, string Message)> RemoveAsync(int userId, int movieId)
    {
        var item = await _watchlistRepository.GetAsync(userId, movieId);
        if (item == null)
            return (false, "Movie not found in watchlist.");

        await _watchlistRepository.RemoveAsync(item);
        return (true, "Movie removed from watchlist.");
    }
}
