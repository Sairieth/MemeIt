using MemeIt.Models.Entities;

namespace MemeIt.Core;

public interface IUserRepository
{
    Task<User?> GetUserAsync(long userId);
    Task AddUserAsync(User user);
    Task RemoveUserAsync(long userId);
    Task<bool> IsUsernameUniqueAsync(string username);
    Task<bool> IsPasswordValidAsync(string password);
    Task<bool> IsEmailUniqueAsync(string email);
    Task EditPasswordAsync(long userId, string newPassword);
    Task EditEmailAsync(long userId, string email);


}