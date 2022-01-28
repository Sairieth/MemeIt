using MemeIt.Core;
using MemeIt.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MemeIt.Data.Repositories;

public class MemeRepository : IMemeRepository
{
    private readonly AppDbContext _db;

    public MemeRepository(AppDbContext db)
    {
        _db = db;
    }

    public virtual async Task<List<Meme>?> GetUsersMemesAsync(long userId)
    {
        return await _db.Memes.Where(m => m.User.Id.Equals(userId) && m.DeletedOn.Equals(DateTime.MinValue))
            .ToListAsync();
    }

    public virtual async Task<List<Meme>?> GetMemesByTagAsync(string tag)
    {
        return await _db.Memes.Include(x => x.User)
            .Where(m => m.Tag.Equals(tag) && m.DeletedOn.Equals(DateTime.MinValue)).ToListAsync();
    }

    public async Task<Meme?> GetMemeByIdAsync(long memeId)
    {
        return await _db.Memes.SingleOrDefaultAsync(x => x.Id == memeId);
    }

    public virtual async Task AddMemeAsync(Meme meme)
    {
        await _db.Memes.AddAsync(meme);
        await _db.SaveChangesAsync();
    }

    public virtual async Task RemoveMemeAsync(long memeId)
    {
        var meme = await _db.Memes.SingleOrDefaultAsync(m =>
            m.Id.Equals(memeId) && m.DeletedOn.Equals(DateTime.MinValue));

        if (meme == null) return;

        meme.DeletedOn = DateTime.Now;

        _db.Entry(meme).Property("DeletedOn").IsModified = true;
        await _db.SaveChangesAsync();
    }

    public virtual async Task EditMemeTitleAsync(string newTitle, long memeId)
    {
        var meme = await _db.Memes.SingleOrDefaultAsync(m =>
            m.Id.Equals(memeId) && m.DeletedOn.Equals(DateTime.MinValue));

        if (meme == null) return;

        meme.Title = newTitle;
        meme.LastModified = DateTime.Now;

        _db.Entry(meme).Property("LastModified").IsModified = true;
        _db.Entry(meme).Property("Title").IsModified = true;

        await _db.SaveChangesAsync();
    }

    public virtual async Task EditMemeTagAsync(string newTag, long memeId)
    {
        var meme = await _db.Memes.SingleOrDefaultAsync(m =>
            m.Id.Equals(memeId) && m.DeletedOn.Equals(DateTime.MinValue));

        if (meme == null) return;

        meme.Tag = newTag;
        meme.LastModified = DateTime.Now;

        _db.Entry(meme).Property("LastModified").IsModified = true;
        _db.Entry(meme).Property("Tag").IsModified = true;

        await _db.SaveChangesAsync();
    }
}