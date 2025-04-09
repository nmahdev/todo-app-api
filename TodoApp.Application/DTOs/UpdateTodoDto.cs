using TodoApp.Domain.ValueObject;

namespace TodoApp.Application.DTOs;

public class UpdateTodoDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public TodoPriority Priority { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? DueDate { get; set; }
}