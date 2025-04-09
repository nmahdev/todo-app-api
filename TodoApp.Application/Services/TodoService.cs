using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Interfaces;

namespace TodoApp.Application.Services;

public class TodoService(ITodoRepository todoRepository) : ITodoService
{
    
    private readonly ITodoRepository _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));

    public async Task<IEnumerable<TodoDto>> GetAllTodosAsync(Guid userId)
    {
        var todos = await _todoRepository.GetByUserIdAsync(userId);
        return todos.Select(MapToDto);
    }

    public async Task<TodoDto> GetTodoByIdAsync(Guid id, Guid userId)
    {
        var todo = await _todoRepository.GetByIdAsync(id);
        
        if (todo == null || todo.UserId != userId)
        {
            return null;
        }

        return MapToDto(todo);
    }

    public async Task<IEnumerable<TodoDto>> GetCompletedTodosAsync(Guid userId)
    {
        var todos = await _todoRepository.GetCompletedByUserIdAsync(userId);
        return todos.Select(MapToDto);
    }

    public async Task<IEnumerable<TodoDto>> GetPendingTodosAsync(Guid userId)
    {
        var todos = await _todoRepository.GetPendingByUserIdAsync(userId);
        return todos.Select(MapToDto);
    }

    public async Task<TodoDto> CreateTodoAsync(CreateTodoDto createTodoDto, Guid userId)
    {
        var todo = new Todo(
            Guid.NewGuid(),
            createTodoDto.Title,
            createTodoDto.Description,
            createTodoDto.Priority,
            createTodoDto.DueDate,
            userId
        );

        await _todoRepository.AddAsync(todo);
        return MapToDto(todo);
    }

    public async Task<TodoDto> UpdateTodoAsync(Guid id, UpdateTodoDto updateTodoDto, Guid userId)
    {
        var todo = await _todoRepository.GetByIdAsync(id);
            
        if (todo == null || todo.UserId != userId)
            return null;

        todo.UpdateTitle(updateTodoDto.Title);
        todo.UpdateDescription(updateTodoDto.Description);
        todo.UpdatePriority(updateTodoDto.Priority);
        todo.UpdateDueDate(updateTodoDto.DueDate);
            
        if (updateTodoDto.IsCompleted)
            todo.MarkAsCompleted();
        else
            todo.MarkAsIncomplete();

        await _todoRepository.UpdateAsync(todo);
        return MapToDto(todo);
    }

    public async Task<bool> DeleteTodoAsync(Guid id, Guid userId)
    {
        var todo = await _todoRepository.GetByIdAsync(id);
            
        if (todo == null || todo.UserId != userId)
            return false;

        await _todoRepository.DeleteAsync(todo);
        return true;
    }
    private TodoDto MapToDto(Todo todo)
    {
        return new TodoDto
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            IsCompleted = todo.IsCompleted,
            Priority = todo.Priority,
            CreatedAt = todo.CreatedAt,
            DueDate = todo.DueDate,
            UserId = todo.UserId
        };
    }
    
}