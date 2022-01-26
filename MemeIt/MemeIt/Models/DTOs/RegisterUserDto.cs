using MemeIt.Models.Common;

namespace MemeIt.Models.DTOs;

public class RegisterUserDto
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; } = null!;
}