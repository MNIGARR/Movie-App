using Microsoft.AspNetCore.Mvc;
using MovieApp.API.DTOs.Auth;
using MovieApp.API.Services;

namespace MovieApp.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    /// <summary>Register a new user</summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var (success, message, token) = await _authService.RegisterAsync(dto);
        if (!success)
            return BadRequest(new { message });

        return Ok(new { message, token });
    }

    /// <summary>Login with email and password</summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var (success, message, token) = await _authService.LoginAsync(dto);
        if (!success)
            return Unauthorized(new { message });

        return Ok(new { message, token });
    }
}
