using TodoApp.Application.DTOs;

namespace TodoApp.Application.Interfaces;

public interface ITodoService
{
    Task<IEnumerable<TodoDto>> GetAllTodosAsync(Guid userId);
    Task<TodoDto> GetTodoByIdAsync(Guid id, Guid userId);
    Task<IEnumerable<TodoDto>> GetCompletedTodosAsync(Guid userId);
    Task<IEnumerable<TodoDto>> GetPendingTodosAsync(Guid userId);
    Task<TodoDto> CreateTodoAsync(CreateTodoDto createTodoDto, Guid userId);
    Task<TodoDto> UpdateTodoAsync(Guid id, UpdateTodoDto updateTodoDto, Guid userId);
    Task<bool> DeleteTodoAsync(Guid id, Guid userId);
}