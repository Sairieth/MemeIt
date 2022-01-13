using MemeIt.Models.Entities;

namespace MemeIt.Data.Repositories.Interfaces;

public interface IMemeRepository
{
    Task<List<Meme>?> GetUsersMemesAsync(long userId);
    Task<List<Meme>?> GetMemesByTagAsync(string tag);
    //Task<Meme> GetMemeWithComments(long memeId);
    Task AddMemeAsync(MemeDto memeDto);
    Task RemoveMemeAsync(long memeId);
    Task EditMemeAsync();

}