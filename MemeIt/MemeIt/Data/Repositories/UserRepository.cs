using MemeIt.Data.Common;
using MemeIt.Data.Services;
using MemeIt.Models.DTOs;
using MemeIt.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MemeIt.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    private readonly AuthService _authService;

    public UserRepository(AppDbContext db, AuthService authService)
    {
        _db = db;
        _authService = authService;
    }

    public async Task<User?> GetUserDetailsAsync(long userId)
    {
        return await _db.Users.SingleOrDefaultAsync(u => u.Id.Equals(userId));
    }

    public async Task AddUserAsync(User userData)
    {
        await _db.Users.AddAsync(userData);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(long userId)
    {
        var user = await _db.Users.SingleAsync(u => u.Id.Equals(userId));
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> IsUsernameUniqueAsync(string username)
    {
        return await _db.Users
            .AnyAsync(u => u.Username.Equals(username))
            .ContinueWith(t => !t.Result);
    }

    public async Task<bool> IsPasswordValidAsync(string password)
    {
        return await _db.Users
            .AnyAsync(u => u.HashedPassword.Equals(_authService.HashPassword(password)))
            .ContinueWith(t => !t.Result);
    }

    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        return await _db.Users
            .AnyAsync(u => u.Email.Equals(email))
            .ContinueWith(t => !t.Result);
    }

    public async Task EditPasswordAsync(long userId, string newPassword)
    {
        var user = await _db.Users.SingleAsync(u => u.Id.Equals(userId));

        user.HashedPassword = _authService.HashPassword(newPassword);

        _db.Entry(user).Property("HashedPassword").IsModified = true;

        await _db.SaveChangesAsync();
    }

    public async Task EditEmailAsync(long userId, string email)
    {
        var user = await _db.Users.SingleAsync(u => u.Id.Equals(userId));

        user.Email = email;

        _db.Entry(user).Property("Email").IsModified = true;

        await _db.SaveChangesAsync();
    }
}