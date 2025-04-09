using TodoApp.Application.DTOs;

namespace TodoApp.Application.Interfaces;

public interface IUserService
{
    Task<UserDto> GetUserByIdAsync(System.Guid id);
    Task<AuthResultDto> RegisterAsync(RegisterDto registerDto);
    Task<AuthResultDto> LoginAsync(LoginDto loginDto);
}