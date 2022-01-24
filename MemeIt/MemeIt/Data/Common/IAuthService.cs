using MemeIt.Models.DTOs;

namespace MemeIt.Data.Common;

public interface IAuthService
{
    AuthData GetAuthData(int id, string role);
    string HashPassword(string password);
    bool VerifyPassword(string? actualPassword, string hashedPassword);
}