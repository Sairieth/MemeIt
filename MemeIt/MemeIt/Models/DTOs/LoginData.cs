namespace MemeIt.Models.DTOs;

public class LoginData
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int Offset { get; set; }
}