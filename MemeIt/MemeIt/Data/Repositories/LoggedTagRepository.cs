using MemeIt.Core;
using MemeIt.Models.Entities;

namespace MemeIt.Data.Repositories;

public class LoggedTagRepository : TagRepository
{
    private readonly ILogger<LoggedTagRepository> _logger;

    public LoggedTagRepository(ILogger<LoggedTagRepository> logger, AppDbContext db) :
        base(db)
    {
        _logger = logger;
    }

    public override async Task<List<Tag>?> GetAllTagsAsync()
    {
        var tags = await base.GetAllTagsAsync();

        if (tags != null)
        {
            _logger.LogInformation("The requested tags: {@tags}", tags.Select(x => x.Title).ToList());
        }

        return tags;
    }
}