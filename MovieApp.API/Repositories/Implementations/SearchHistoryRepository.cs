using Microsoft.EntityFrameworkCore;
using MovieApp.API.Data;
using MovieApp.API.Models;
using MovieApp.API.Repositories.Interfaces;

namespace MovieApp.API.Repositories.Implementations;

public class SearchHistoryRepository : ISearchHistoryRepository
{
    private readonly AppDbContext _context;

    public SearchHistoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SearchHistory>> GetUserHistoryAsync(int userId, string? type)
    {
        var query = _context.SearchHistories.Where(s => s.UserId == userId);

        if (!string.IsNullOrWhiteSpace(type))
            query = query.Where(s => s.Type == type);

        return await query
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();
    }

    public async Task<SearchHistory> AddAsync(SearchHistory searchHistory)
    {
        _context.SearchHistories.Add(searchHistory);
        await _context.SaveChangesAsync();
        return searchHistory;
    }
}
