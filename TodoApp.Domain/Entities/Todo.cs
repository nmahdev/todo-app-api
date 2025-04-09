using TodoApp.Domain.ValueObject;

namespace TodoApp.Domain.Entities;

public class Todo
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public bool IsCompleted { get; private set; }
    public TodoPriority Priority { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? DueDate { get; private set; }
    public Guid UserId { get; private set; }

    private Todo() { }

    public Todo(Guid id, string title, string description, TodoPriority priority, DateTime? dueDate, Guid userId)
    {
        Id = id;
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Description = description;
        Priority = priority;
        IsCompleted = false;
        CreatedAt = DateTime.UtcNow;
        DueDate = dueDate;
        UserId = userId;
    }

    public void MarkAsCompleted()
    {
        IsCompleted = true;
    }

    public void MarkAsIncomplete()
    {
        IsCompleted = false;
    }

    public void UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));

        Title = title;
    }

    public void UpdateDescription(string description)
    {
        Description = description;
    }

    public void UpdatePriority(TodoPriority priority)
    {
        Priority = priority;
    }

    public void UpdateDueDate(DateTime? dueDate)
    {
        DueDate = dueDate;
    }
}