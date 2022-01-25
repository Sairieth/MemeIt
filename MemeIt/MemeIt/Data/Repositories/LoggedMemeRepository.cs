using MemeIt.Core;
using MemeIt.Models.Entities;

namespace MemeIt.Data.Repositories;

public class LoggedMemeRepository : MemeRepository
{
    private readonly ILogger<LoggedMemeRepository> _logger;

    public LoggedMemeRepository(ILogger<LoggedMemeRepository> logger, IMemeRepository memeRepository, AppDbContext db) :
        base(db)
    {
        _logger = logger;
    }

    public override async Task<List<Meme>?> GetUsersMemesAsync(long userId)
    {
        var usersMemes = await base.GetUsersMemesAsync(userId);

        _logger.LogInformation(
            usersMemes != null
                ? "Requested meme(s) by user with ID No.{UsersId} was found"
                : "Requested meme(s) by user with ID No.{UsersId} was not found", userId);

        return usersMemes;
    }

    public override async Task<List<Meme>?> GetMemesByTagAsync(string tag)
    {
        var taggedMemes = await base.GetMemesByTagAsync(tag);

        _logger.LogInformation(
            taggedMemes != null
                ? "Requested meme(s) by tag {Tag} was found"
                : "Requested meme(s) by tag {Tag} was not found", tag);

        return taggedMemes;
    }

    public override async Task AddMemeAsync(Meme meme)
    {
        _logger.LogInformation("Added {@meme} to DB", meme);

        await base.AddMemeAsync(meme);
    }

    public override async Task RemoveMemeAsync(long memeId)
    {
        _logger.LogInformation("Meme with ID No.{memeId} was removed", memeId);

        await base.RemoveMemeAsync(memeId);
    }

    public override async Task EditMemeTitleAsync(string newTitle, long memeId)
    {
        _logger.LogInformation("Edited meme's title with ID No.{memeId} to {newTitle}", memeId, newTitle);

        await base.EditMemeTitleAsync(newTitle, memeId);
    }

    public override async Task EditMemeTagAsync(string newTag, long memeId)
    {
        _logger.LogInformation("Edited meme's tag with ID No.{memeId} to {newTag}", memeId, newTag);

        await base.EditMemeTagAsync(newTag, memeId);
    }
}