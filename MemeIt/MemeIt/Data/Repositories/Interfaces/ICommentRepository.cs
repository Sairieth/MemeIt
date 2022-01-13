using MemeIt.Models.Entities;

namespace MemeIt.Data.Repositories.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>?> GetUsersComments(long userId);
    Task<List<Comment>?> GetMemesComments(long memeId);
    Task AddComment(CommentDto commentDto);
    Task EditComment(CommentDto commentDto);
    Task DeleteComment(long commentId);

}