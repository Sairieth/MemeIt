using MemeIt.Data.Repositories.Interfaces;
using MemeIt.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MemeIt.Data.Repositories;

public class CommentRepository:ICommentRepository
{
    private readonly AppDbContext _db;

    public CommentRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Comment>?> GetUsersCommentsAsync(long userId)
    {
        return await _db.Comments.Where(c => c.UserId.Equals(userId)).ToListAsync();
    }

    public async Task<List<Comment>?> GetMemesCommentsAsync(long memeId)
    {
        return await _db.Comments.Where(c => c.MemeId.Equals(memeId)).ToListAsync();
    }

    public async Task AddCommentAsync(Comment comment)
    {
        await _db.Comments.AddAsync(comment);
        await _db.SaveChangesAsync();
    }

    public async Task EditCommentAsync(string newMessage,long commentId)
    {
        var comment = await _db.Comments.SingleAsync(u => u.Id.Equals(commentId));

        comment.Message = newMessage;

        _db.Entry(comment).Property("Message").IsModified = true;

        await _db.SaveChangesAsync();
    }

    public async Task DeleteCommentAsync(long commentId)
    {
        var comment = await _db.Comments.SingleAsync(u => u.Id.Equals(commentId));

        _db.Remove(comment);

        await _db.SaveChangesAsync();
    }
}