using MemeIt.Core;
using MemeIt.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MemeIt.Data.Repositories;

public class TagRepository:ITagRepository
{
    private readonly AppDbContext _db;

    public TagRepository(AppDbContext db)
    {
        _db = db;
    }

    public virtual async Task<List<Tag>> GetAllTagsAsync()
    {
        return await _db.Tags.ToListAsync();
    }
}