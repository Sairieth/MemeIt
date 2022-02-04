namespace MemeIt.Models.DTOs;

public class AuthData
{
    public string Token { get; set; } = null!;
    public DateTime TokenExpirationTime { get; set; }
    public long Id { get; set; }
    public string UserRole { get; set; } = null!;
    public string UserName { get; set; } = null!;
}