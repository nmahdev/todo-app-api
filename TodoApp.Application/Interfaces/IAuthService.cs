using TodoApp.Domain.Entities;

namespace TodoApp.Application.Interfaces;

public interface IAuthService
{
    Task<string> GenerateJwtToken(User user);
    Task<string> GenerateRefreshToken();
    Task<Guid> ValidateJwtToken(string token);
}