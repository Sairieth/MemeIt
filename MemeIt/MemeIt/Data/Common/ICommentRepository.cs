using MemeIt.Models.Entities;

namespace MemeIt.Data.Common;

public interface ICommentRepository
{
    Task<List<Comment>?> GetUsersCommentsAsync(long userId);
    Task<List<Comment>?> GetMemesCommentsAsync(long memeId);
    Task AddCommentAsync(Comment comment);
    Task EditCommentAsync(string newMessage,long commentId);
    Task DeleteCommentAsync(long commentId);

}