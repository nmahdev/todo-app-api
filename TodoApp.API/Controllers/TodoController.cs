using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces;

namespace TodoApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TodosController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodosController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoDto>>> GetAllTodos()
    {
        var userId = GetUserId();
        var todos = await _todoService.GetAllTodosAsync(userId);
        return Ok(todos);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TodoDto>> GetTodoById(Guid id)
    {
        var userId = GetUserId();
        var todo = await _todoService.GetTodoByIdAsync(id, userId);

        if (todo == null)
            return NotFound();

        return Ok(todo);
    }

    [HttpGet("completed")]
    public async Task<ActionResult<IEnumerable<TodoDto>>> GetCompletedTodos()
    {
        var userId = GetUserId();
        var todos = await _todoService.GetCompletedTodosAsync(userId);
        return Ok(todos);
    }

    [HttpGet("pending")]
    public async Task<ActionResult<IEnumerable<TodoDto>>> GetPendingTodos()
    {
        var userId = GetUserId();
        var todos = await _todoService.GetPendingTodosAsync(userId);
        return Ok(todos);
    }

    [HttpPost]
    public async Task<ActionResult<TodoDto>> CreateTodo(CreateTodoDto createTodoDto)
    {
        var userId = GetUserId();
        var todo = await _todoService.CreateTodoAsync(createTodoDto, userId);
        return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TodoDto>> UpdateTodo(Guid id, UpdateTodoDto updateTodoDto)
    {
        var userId = GetUserId();
        var todo = await _todoService.UpdateTodoAsync(id, updateTodoDto, userId);

        if (todo == null)
            return NotFound();

        return Ok(todo);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteTodo(Guid id)
    {
        var userId = GetUserId();
        var result = await _todoService.DeleteTodoAsync(id, userId);

        if (!result)
            return NotFound();

        return NoContent();
    }

    private Guid GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.Parse(userIdClaim);
    }
}