using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.API.DTOs;
using MovieApp.API.Services;

namespace MovieApp.API.Controllers;

[ApiController]
[Route("api/movies")]
public class MoviesController : ControllerBase
{
    private readonly MovieService _movieService;

    public MoviesController(MovieService movieService)
    {
        _movieService = movieService;
    }

    /// <summary>Get all movies with pagination</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var movies = await _movieService.GetAllAsync(page, pageSize);
        return Ok(movies);
    }

    /// <summary>Get a movie by ID</summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var movie = await _movieService.GetByIdAsync(id);
        if (movie == null)
            return NotFound(new { message = "Movie not found." });

        return Ok(movie);
    }

    /// <summary>Search movies by query with pagination</summary>
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string query, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        if (string.IsNullOrWhiteSpace(query))
            return BadRequest(new { message = "Search query cannot be empty." });

        var movies = await _movieService.SearchAsync(query, page, pageSize);
        return Ok(movies);
    }

    /// <summary>Add a new movie (Admin only)</summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Add([FromBody] CreateMovieDto dto)
    {
        var movie = await _movieService.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie);
    }
}
