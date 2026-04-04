using MovieApp.API.Models;

namespace MovieApp.API.Repositories.Interfaces;

public interface IWatchlistRepository
{
    Task<IEnumerable<Watchlist>> GetUserWatchlistAsync(int userId);
    Task<Watchlist?> GetAsync(int userId, int movieId);
    Task<Watchlist> AddAsync(Watchlist watchlist);
    Task RemoveAsync(Watchlist watchlist);
}
