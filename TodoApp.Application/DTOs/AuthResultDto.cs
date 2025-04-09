namespace TodoApp.Application.DTOs;

public class AuthResultDto
{
    public UserDto User { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}