using MemeIt.Models.DTOs;
using MemeIt.Models.Entities;

namespace MemeIt.Data.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserDetailsAsync(long userId);
    Task AddUserAsync(User user);
    Task DeleteUserAsync(long userId);
    Task<bool> IsUsernameUniqueAsync(string username);
    Task<bool> IsPasswordValidAsync(string password);
    Task<bool> IsEmailUniqueAsync(string email);
    Task EditPasswordAsync(long userId, string newPassword);
    Task EditEmailAsync(long userId, string email);


}