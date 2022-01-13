using MemeIt.Models.Entities;

namespace MemeIt.Data.Repositories.Interfaces;

public interface IMemeRepository
{
    Task<List<Meme>?> GetUsersMemes(long userId);
    Task<List<Meme>?> GetMemesByTag(string tag);
    //Task<Meme> GetMemeWithComments(long memeId);
    Task AddMeme(MemeDto memeDto);
    Task RemoveMeme(long memeId);
    Task EditMeme();

}