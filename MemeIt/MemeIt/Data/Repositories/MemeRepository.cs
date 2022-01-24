using MemeIt.Data.Common;
using MemeIt.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MemeIt.Data.Repositories;

public class MemeRepository:IMemeRepository
{
    private readonly AppDbContext _db;

    public MemeRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Meme>?> GetUsersMemesAsync(long userId)
    {
        return await _db.Memes.Where(m=>m.UserId.Equals(userId)).ToListAsync();
    }

    public async Task<List<Meme>?> GetMemesByTagAsync(string tag)
    {
        return await _db.Memes.Where(m => m.Tag.Equals(tag)).ToListAsync();
    }

    public async Task AddMemeAsync(Meme meme)
    {
        await _db.Memes.AddAsync(meme);
        await _db.SaveChangesAsync();
    }

    public async Task RemoveMemeAsync(long memeId)
    {
        var meme = await _db.Memes.SingleAsync(u => u.Id.Equals(memeId));
        _db.Memes.Remove(meme);
        await _db.SaveChangesAsync();
    }

    public async Task EditMemeTitleAsync(string newTitle,long memeId)
    {
        var meme = await _db.Memes.SingleAsync(u => u.Id.Equals(memeId));

        meme.Title = newTitle;

        _db.Entry(meme).Property("Title").IsModified = true;

        await _db.SaveChangesAsync();
    }

    public async Task EditMemeTagAsync(string newTag, long memeId)
    {
        var meme = await _db.Memes.SingleAsync(u => u.Id.Equals(memeId));

        meme.Tag = newTag;

        _db.Entry(meme).Property("Tag").IsModified = true;

        await _db.SaveChangesAsync();
    }
}