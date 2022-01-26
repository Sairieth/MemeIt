using MemeIt.Core;
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

    public virtual async Task<List<Comment>?> GetUsersCommentsAsync(long userId)
    {
        return await _db.Comments.Where(c => c.User.Id.Equals(userId) && c.DeletedOn != DateTime.MinValue).ToListAsync();
    }

    public virtual async Task<List<Comment>?> GetMemesCommentsAsync(long memeId)
    {
        return await _db.Comments.Where(c => c.Meme.Id.Equals(memeId) && c.DeletedOn != DateTime.MinValue).ToListAsync();
    }

    public virtual async Task AddCommentAsync(Comment comment)
    {
        await _db.Comments.AddAsync(comment);
        await _db.SaveChangesAsync();
    }

    public virtual async Task EditCommentAsync(string newMessage,long commentId)
    {
        var comment = await _db.Comments.SingleAsync(u => u.Id.Equals(commentId));

        comment.Message = newMessage;
        comment.LastModified = DateTime.Now;

        _db.Entry(comment).Property("Message").IsModified = true;
        _db.Entry(comment).Property("LastModified").IsModified = true;

        await _db.SaveChangesAsync();
    }

    public virtual async Task DeleteCommentAsync(long commentId)
    {
        var comment = await _db.Comments.SingleAsync(u => u.Id.Equals(commentId));

        comment.DeletedOn = DateTime.Now;
        _db.Entry(comment).Property("DeletedOn").IsModified = true;

        await _db.SaveChangesAsync();
    }
}