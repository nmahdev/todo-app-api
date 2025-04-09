using TodoApp.Domain.Entities;

namespace TodoApp.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByUsernameAsync(string username);
    Task<User> GetByEmailAsync(string email);
    Task<bool> UsernameExistsAsync(string username);
    Task<bool> EmailExistsAsync(string email);
}