using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Interfaces;
using TodoApp.Infrastructure.Repositories;

namespace TodoApp.Infrastructure.Data;

public class TodoRepository : Repository<Todo>, ITodoRepository
{
    public TodoRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Todo>> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet.Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Todo>> GetCompletedByUserIdAsync(Guid userId)
    {
        return await _dbSet.Where(t => t.UserId == userId && t.IsCompleted)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Todo>> GetPendingByUserIdAsync(Guid userId)
    {
        return await _dbSet.Where(t => t.UserId == userId && !t.IsCompleted)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }
}