using MemeIt.Models.DTOs;

namespace MemeIt.Data.Serices;

public interface IAuthService
{
    AuthData GetAuthData(int id, string role);
    string HashPassword(string password);
    bool VerifyPassword(string? actualPassword, string hashedPassword);
}