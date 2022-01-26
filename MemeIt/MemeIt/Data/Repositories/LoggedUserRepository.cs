using MemeIt.Data.Services;
using MemeIt.Models.Entities;

namespace MemeIt.Data.Repositories;

public class LoggedUserRepository : UserRepository
{
    private readonly ILogger<LoggedUserRepository> _logger;

    public LoggedUserRepository(ILogger<LoggedUserRepository> logger, AppDbContext db) : base(
        db)
    {
        _logger = logger;
    }


    public override async Task<User?> GetUserAsync(long userId)
    {
        var user = await base.GetUserAsync(userId);

        _logger.LogInformation(
            user != null
                ? "Requested user with ID No.{UsersId} was found: {@user}"
                : "Requested user with ID No.{UsersId} was not found: {@user}", userId, user);

        return user;
    }

    public override async Task AddUserAsync(User user)
    {
        _logger.LogInformation("Added {@user} to DB", user);

        await base.AddUserAsync(user);
    }

    public override async Task RemoveUserAsync(long userId)
    {
        _logger.LogInformation("User with ID No.{memeId} was removed", userId);

        await base.RemoveUserAsync(userId);
    }

    public override async Task<bool> IsUsernameUniqueAsync(string username)
    {
        var result = await base.IsUsernameUniqueAsync(username);
        _logger.LogInformation(result ? "" : "", username);
        return result;
    }

    public override async Task<bool> IsPasswordValidAsync(string password)
    {
        var result = await base.IsPasswordValidAsync(password);

        return result;
    }

    public override async Task<bool> IsEmailUniqueAsync(string email)
    {
        var result = await base.IsEmailUniqueAsync(email);

        return result;
    }

    public override async Task EditPasswordAsync(long userId, string newPassword)
    {
        _logger.LogInformation("Edited User's password with ID No.{memeId} to {newPassword}", userId, newPassword);

        await base.EditPasswordAsync(userId, newPassword);
    }

    public override async Task EditEmailAsync(long userId, string email)
    {
        _logger.LogInformation("Edited User's email with ID No.{memeId} to {email}", userId, email);

        await base.EditEmailAsync(userId, email);
    }
}