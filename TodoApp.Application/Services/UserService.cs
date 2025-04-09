using Microsoft.AspNetCore.Identity;
using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Interfaces;

namespace TodoApp.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;
    private readonly IPasswordHasher _passwordHasher;
    
    public UserService(IUserRepository userRepository, IAuthService authService, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
    }
    
    public async Task<UserDto> GetUserByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
        {
            return null;
        }

        return MapToDto(user);
    }

    public async Task<AuthResultDto> RegisterAsync(RegisterDto registerDto)
    {
        if (await _userRepository.UsernameExistsAsync(registerDto.Username))
        {
            throw new ApplicationException("Username is already exists.");
        }
        
        if (await _userRepository.EmailExistsAsync(registerDto.Email))
        {
            throw new ApplicationException("Email is already exists.");
        }
        
        var passwordHash = _passwordHasher.HashPassword(registerDto.Password);
            
        var user = new User(
            Guid.NewGuid(),
            registerDto.Username,
            registerDto.Email,
            passwordHash
        );

        await _userRepository.AddAsync(user);

        var token = await _authService.GenerateJwtToken(user);
        var refreshToken = await _authService.GenerateRefreshToken();

        return new AuthResultDto
        {
            User = MapToDto(user),
            Token = token,
            RefreshToken = refreshToken
        };
    }

    public async Task<AuthResultDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.GetByUsernameAsync(loginDto.Username);
            
        if (user == null)
            throw new ApplicationException("Invalid username or password");

        if (!_passwordHasher.VerifyPassword(loginDto.Password, user.PasswordHash))
            throw new ApplicationException("Invalid username or password");

        var token = await _authService.GenerateJwtToken(user);
        var refreshToken = await _authService.GenerateRefreshToken();

        return new AuthResultDto
        {
            User = MapToDto(user),
            Token = token,
            RefreshToken = refreshToken
        };
    }
    
    private UserDto MapToDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        };
    }
}