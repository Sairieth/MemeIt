using MemeIt.Models.Entities;

namespace MemeIt.Core;

public interface IMemeRepository
{
    Task<List<Meme>?> GetUsersMemesAsync(long userId);
    Task<List<Meme>?> GetMemesByTagAsync(string tag);

    Task<Meme?> GetMemeByIdAsync(long memeId);
    Task AddMemeAsync(Meme meme);
    Task RemoveMemeAsync(long memeId);
    Task EditMemeTitleAsync(string newTitle, long memeId);
    Task EditMemeTagAsync(string newTag, long memeId);

}