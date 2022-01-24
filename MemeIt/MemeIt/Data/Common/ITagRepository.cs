using MemeIt.Models.Entities;

namespace MemeIt.Data.Common;

public interface ITagRepository
{
    Task<List<Tag>> GetAllTagsAsync();
}