namespace MemeIt.Models.DTOs;

public class AuthData
{
    public string Token { get; set; } = null!;
    public DateTime TokenExpirationTime { get; set; }
    
}