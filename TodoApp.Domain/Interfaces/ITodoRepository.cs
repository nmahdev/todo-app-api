using TodoApp.Domain.Entities;

namespace TodoApp.Domain.Interfaces;

public interface ITodoRepository : IRepository<Todo>
{
    Task<IEnumerable<Todo>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<Todo>> GetCompletedByUserIdAsync(Guid userId);
    Task<IEnumerable<Todo>> GetPendingByUserIdAsync(Guid userId);
}