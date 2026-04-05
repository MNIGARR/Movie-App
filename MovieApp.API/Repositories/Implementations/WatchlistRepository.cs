using Microsoft.EntityFrameworkCore;
using MovieApp.API.Data;
using MovieApp.API.Models;
using MovieApp.API.Repositories.Interfaces;

namespace MovieApp.API.Repositories.Implementations;

public class WatchlistRepository : IWatchlistRepository
{
    private readonly AppDbContext _context;

    public WatchlistRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Watchlist>> GetUserWatchlistAsync(int userId)
    {
        return await _context.Watchlists
            .Include(w => w.Movie)
            .Where(w => w.UserId == userId)
            .OrderByDescending(w => w.CreatedAt)
            .ToListAsync();
    }

    public async Task<Watchlist?> GetAsync(int userId, int movieId)
    {
        return await _context.Watchlists
            .FirstOrDefaultAsync(w => w.UserId == userId && w.MovieId == movieId);
    }

    public async Task<Watchlist> AddAsync(Watchlist watchlist)
    {
        _context.Watchlists.Add(watchlist);
        await _context.SaveChangesAsync();
        return watchlist;
    }

    public async Task RemoveAsync(Watchlist watchlist)
    {
        _context.Watchlists.Remove(watchlist);
        await _context.SaveChangesAsync();
    }
}
