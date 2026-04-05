using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.API.Services;

namespace MovieApp.API.Controllers;

[ApiController]
[Route("api/watchlist")]
[Authorize]
public class WatchlistController : BaseController
{
    private readonly WatchlistService _watchlistService;

    public WatchlistController(WatchlistService watchlistService)
    {
        _watchlistService = watchlistService;
    }

    /// <summary>Get the current user's watchlist</summary>
    [HttpGet]
    public async Task<IActionResult> GetWatchlist()
    {
        var items = await _watchlistService.GetUserWatchlistAsync(GetUserId());
        return Ok(items);
    }

    /// <summary>Add a movie to the watchlist</summary>
    [HttpPost("{movieId:int}")]
    public async Task<IActionResult> AddToWatchlist(int movieId)
    {
        var (success, message, item) = await _watchlistService.AddAsync(GetUserId(), movieId);
        if (!success)
            return BadRequest(new { message });

        return Ok(new { message, item });
    }

    /// <summary>Remove a movie from the watchlist</summary>
    [HttpDelete("{movieId:int}")]
    public async Task<IActionResult> RemoveFromWatchlist(int movieId)
    {
        var (success, message) = await _watchlistService.RemoveAsync(GetUserId(), movieId);
        if (!success)
            return NotFound(new { message });

        return Ok(new { message });
    }
}
