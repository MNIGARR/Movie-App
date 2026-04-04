using MovieApp.API.DTOs;
using MovieApp.API.Models;
using MovieApp.API.Repositories.Interfaces;

namespace MovieApp.API.Services;

public class MovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<IEnumerable<MovieDto>> GetAllAsync(int page, int pageSize)
    {
        var movies = await _movieRepository.GetAllAsync(page, pageSize);
        return movies.Select(MapToDto);
    }

    public async Task<MovieDto?> GetByIdAsync(int id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        return movie == null ? null : MapToDto(movie);
    }

    public async Task<IEnumerable<MovieDto>> SearchAsync(string query, int page, int pageSize)
    {
        var movies = await _movieRepository.SearchAsync(query, page, pageSize);
        return movies.Select(MapToDto);
    }

    public async Task<MovieDto> AddAsync(CreateMovieDto dto)
    {
        var movie = new Movie
        {
            Title = dto.Title,
            Overview = dto.Overview,
            PosterPath = dto.PosterPath,
            ReleaseDate = dto.ReleaseDate,
            VoteAverage = dto.VoteAverage,
            Popularity = dto.Popularity
        };

        var created = await _movieRepository.AddAsync(movie);
        return MapToDto(created);
    }

    private static MovieDto MapToDto(Movie movie) => new()
    {
        Id = movie.Id,
        Title = movie.Title,
        Overview = movie.Overview,
        PosterPath = movie.PosterPath,
        ReleaseDate = movie.ReleaseDate,
        VoteAverage = movie.VoteAverage,
        Popularity = movie.Popularity
    };
}
