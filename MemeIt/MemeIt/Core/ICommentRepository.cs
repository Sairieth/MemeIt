using MemeIt.Models.Entities;

namespace MemeIt.Core;

public interface ICommentRepository
{
    Task<Comment?> GetCommentAsync(long commentId);
    Task<List<Comment>?> GetUsersCommentsAsync(long userId);
    Task<List<Comment>?> GetMemesCommentsAsync(long memeId);
    Task AddCommentAsync(Comment? comment);
    Task EditCommentAsync(string newMessage,long commentId);
    Task DeleteCommentAsync(long commentId);

}