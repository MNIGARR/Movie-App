using MovieApp.API.Models;

namespace MovieApp.API.Repositories.Interfaces;

public interface ISearchHistoryRepository
{
    Task<IEnumerable<SearchHistory>> GetUserHistoryAsync(int userId, string? type);
    Task<SearchHistory> AddAsync(SearchHistory searchHistory);
}
