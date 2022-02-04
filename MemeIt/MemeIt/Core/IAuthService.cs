using MemeIt.Models.DTOs;

namespace MemeIt.Core;

public interface IAuthService
{
    AuthData GetAuthData(long id, string role, string userName);
    string HashPassword(string password);
    bool VerifyPassword(string? actualPassword, string hashedPassword);
    bool IsValidId(string jwtToken,long userId);
}