using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces;

namespace TodoApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResultDto>> Register(RegisterDto registerDto)
    {
        try
        {
            var authResult = await _userService.RegisterAsync(registerDto);
            return Ok(authResult);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResultDto>> Login(LoginDto loginDto)
    {
        try
        {
            var authResult = await _userService.LoginAsync(loginDto);
            return Ok(authResult);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var userId = System.Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
        var user = await _userService.GetUserByIdAsync(userId);

        if (user == null)
            return NotFound();

        return Ok(user);
    }
}