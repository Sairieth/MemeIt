using MemeIt.Models.Entities;

namespace MemeIt.Data.Repositories.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>?> GetUsersCommentsAsync(long userId);
    Task<List<Comment>?> GetMemesCommentsAsync(long memeId);
    Task AddCommentAsync(CommentDto commentDto);
    Task EditCommentAsync(CommentDto commentDto);
    Task DeleteCommentAsync(long commentId);

}