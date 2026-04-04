using MovieApp.API.Models;

namespace MovieApp.API.Repositories.Interfaces;

public interface IMovieRepository
{
    Task<IEnumerable<Movie>> GetAllAsync(int page, int pageSize);
    Task<Movie?> GetByIdAsync(int id);
    Task<IEnumerable<Movie>> SearchAsync(string query, int page, int pageSize);
    Task<Movie> AddAsync(Movie movie);
    Task<bool> ExistsAsync(int id);
}
