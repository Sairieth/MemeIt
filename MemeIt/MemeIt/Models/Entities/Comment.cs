using MemeIt.Models.Common;

namespace MemeIt.Models.Entities;

public class Comment : EntityWithModificationDate
{
    public string Message { get; set; } = null!;
    public string? Flag { get; set; } = default;
    public long MemeId { get; set; }
    public long UserId { get; set; }
}