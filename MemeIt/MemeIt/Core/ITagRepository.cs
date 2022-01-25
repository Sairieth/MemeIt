using MemeIt.Models.Entities;

namespace MemeIt.Core;

public interface ITagRepository
{
    Task<List<Tag>> GetAllTagsAsync();
}