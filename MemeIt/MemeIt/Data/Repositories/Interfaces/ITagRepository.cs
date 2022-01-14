using MemeIt.Models.Entities;

namespace MemeIt.Data.Repositories.Interfaces;

public interface ITagRepository
{
    Task<List<Tag>> GetAllTagsAsync();
}