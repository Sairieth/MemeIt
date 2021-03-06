using MemeIt.Data.Services;
using MemeIt.Models.DTOs;
using MemeIt.Models.Entities;
using Microsoft.EntityFrameworkCore;
using CryptoHelper;
using MemeIt.Core;

namespace MemeIt.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public virtual async Task<User?> GetUserAsync(long userId)
    {
        return await _db.Users
            .SingleOrDefaultAsync(u => u.Id.Equals(userId) && u.DeletedOn.Equals(DateTime.MinValue));
    }

    public virtual async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _db.Users
            .SingleOrDefaultAsync(u =>
            u.Username.Equals(username) && u.DeletedOn.Equals(DateTime.MinValue));
    }

    public virtual async Task AddUserAsync(User userData)
    {
        await _db.Users.AddAsync(userData);
        await _db.SaveChangesAsync();
    }

    public virtual async Task RemoveUserAsync(long userId)
    {
        var user = await _db.Users.SingleOrDefaultAsync(u =>
            u.Id.Equals(userId) && u.DeletedOn.Equals(DateTime.MinValue));

        if (user == null) return;

        user.DeletedOn = DateTime.Now;

        _db.Entry(user).Property("DeletedOn").IsModified = true;

        await _db.SaveChangesAsync();
    }

    public virtual async Task<bool> IsUsernameUniqueAsync(string username)
    {
        return await _db.Users
            .AnyAsync(u => u.Username.Equals(username))
            .ContinueWith(t => !t.Result);
    }

    public virtual async Task<bool> IsPasswordValidAsync(string password)
    {
        return await _db.Users
            .AnyAsync(u => u.HashedPassword.Equals(Crypto.HashPassword(password)))
            .ContinueWith(t => !t.Result);
    }

    public virtual async Task<bool> IsEmailUniqueAsync(string email)
    {
        return await _db.Users
            .AnyAsync(u => u.Email.Equals(email))
            .ContinueWith(t => !t.Result);
    }

    public virtual async Task EditPasswordAsync(long userId, string newPassword)
    {
        var user = await _db.Users.SingleOrDefaultAsync(u =>
            u.Id.Equals(userId) && u.DeletedOn.Equals(DateTime.MinValue));

        if (user == null) return;

        user.LastModified = DateTime.Now;


        user.HashedPassword = Crypto.HashPassword(newPassword);

        _db.Entry(user).Property("LastModified").IsModified = true;
        _db.Entry(user).Property("HashedPassword").IsModified = true;

        await _db.SaveChangesAsync();
    }

    public virtual async Task EditEmailAsync(long userId, string email)
    {
        var user = await _db.Users.SingleOrDefaultAsync(u =>
            u.Id.Equals(userId) && u.DeletedOn.Equals(DateTime.MinValue));

        if (user == null) return;

        user.Email = email;
        user.LastModified = DateTime.Now;

        _db.Entry(user).Property("LastModified").IsModified = true;
        _db.Entry(user).Property("Email").IsModified = true;

        await _db.SaveChangesAsync();
    }
}