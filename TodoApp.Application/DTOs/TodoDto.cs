using TodoApp.Domain.ValueObject;

namespace TodoApp.Application.DTOs;

public class TodoDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public TodoPriority Priority { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DueDate { get; set; }
    public Guid UserId { get; set; }
}