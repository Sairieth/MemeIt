using MemeIt.Models.Entities;

namespace MemeIt.Data.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserDetails(long userId);
    Task AddUser(UserDto userData);
    Task DeleteUser(long userId);
    Task<bool> IsUsernameValid(string email);
    Task<bool> IsPasswordValid(string password);
    Task<bool> IsEmailValid(string email);
    Task EditPassword(long userId, string newPassword);
    Task EditEmail(long userId, string email);


}