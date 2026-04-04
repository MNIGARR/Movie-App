using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.API.DTOs;
using MovieApp.API.Models;
using MovieApp.API.Repositories.Interfaces;

namespace MovieApp.API.Controllers;

[ApiController]
[Route("api/search")]
[Authorize]
public class SearchController : BaseController
{
    private readonly ISearchHistoryRepository _searchHistoryRepository;

    public SearchController(ISearchHistoryRepository searchHistoryRepository)
    {
        _searchHistoryRepository = searchHistoryRepository;
    }

    /// <summary>Get search history for the current user, optionally filtered by type (movie, tv, person)</summary>
    [HttpGet("history")]
    public async Task<IActionResult> GetHistory([FromQuery] string? type)
    {
        var history = await _searchHistoryRepository.GetUserHistoryAsync(GetUserId(), type);
        return Ok(history);
    }

    /// <summary>Save a search query to history</summary>
    [HttpPost("history")]
    public async Task<IActionResult> SaveSearch([FromBody] SaveSearchDto dto)
    {
        var entry = new SearchHistory
        {
            UserId = GetUserId(),
            Query = dto.Query,
            Type = dto.Type
        };

        var result = await _searchHistoryRepository.AddAsync(entry);
        return Ok(result);
    }
}
