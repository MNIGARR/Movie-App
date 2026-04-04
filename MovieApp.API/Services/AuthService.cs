using MovieApp.API.DTOs.Auth;
using MovieApp.API.Helpers;
using MovieApp.API.Models;
using MovieApp.API.Repositories.Interfaces;

namespace MovieApp.API.Services;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtHelper _jwtHelper;

    public AuthService(IUserRepository userRepository, JwtHelper jwtHelper)
    {
        _userRepository = userRepository;
        _jwtHelper = jwtHelper;
    }

    public async Task<(bool Success, string Message, string? Token)> RegisterAsync(RegisterDto dto)
    {
        if (await _userRepository.EmailExistsAsync(dto.Email))
            return (false, "Email already in use.", null);

        if (await _userRepository.UsernameExistsAsync(dto.Username))
            return (false, "Username already taken.", null);

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = PasswordHasher.Hash(dto.Password)
        };

        await _userRepository.AddAsync(user);
        var token = _jwtHelper.GenerateToken(user);
        return (true, "Registration successful.", token);
    }

    public async Task<(bool Success, string Message, string? Token)> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        if (user == null || !PasswordHasher.Verify(dto.Password, user.PasswordHash))
            return (false, "Invalid email or password.", null);

        var token = _jwtHelper.GenerateToken(user);
        return (true, "Login successful.", token);
    }
}
