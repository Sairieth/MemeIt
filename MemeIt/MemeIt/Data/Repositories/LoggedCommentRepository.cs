using MemeIt.Core;
using MemeIt.Models.Entities;
using Serilog.Context;

namespace MemeIt.Data.Repositories;

public class LoggedCommentRepository:CommentRepository
{
    private readonly ILogger<LoggedCommentRepository> _logger;

    public LoggedCommentRepository(ILogger<LoggedCommentRepository> logger,AppDbContext db):base(db)
    {
        _logger = logger;
    }

    public override async Task<List<Comment>?> GetUsersCommentsAsync(long userId)
    {
        var usersComments = await base.GetUsersCommentsAsync(userId);

        _logger.LogInformation(
            usersComments != null
                ? "Requested comment(s) by user with ID No.{UsersId} was found"
                : "Requested comment(s) by user with ID No.{UsersId} was not found", userId);

        return usersComments;
    }

    public override async Task<List<Comment>?> GetMemesCommentsAsync(long memeId)
    {
        var memesComments = await base.GetMemesCommentsAsync(memeId);

        _logger.LogInformation(
            memesComments != null
                ? "{count} comment(s) for meme with ID {memeId} was found"
                : "Requested comment(s) for meme with ID {memeId} was not found", memeId);

        return memesComments;
    }

    public override async Task AddCommentAsync(Comment comment)
    {
        _logger.LogInformation("Added {@comment} to meme with ID No.{memeId}", comment,comment.Meme.Id);
        await base.AddCommentAsync(comment);
    }

    public override async Task EditCommentAsync(string newMessage, long commentId)
    {
        _logger.LogInformation("Edited message of comment with ID No.{memeId} to {newMessage}", commentId,newMessage);
        await base.EditCommentAsync(newMessage,commentId);
    }

    public override async Task DeleteCommentAsync(long commentId)
    {
        _logger.LogInformation("Comment with ID: No.{commentID} was removed", commentId);
        await base.DeleteCommentAsync(commentId);
    }
}