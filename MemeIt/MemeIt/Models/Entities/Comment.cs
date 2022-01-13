using MemeIt.Models.Common;

namespace MemeIt.Models.Entities;

public class Comment : EntityWithModificationDate
{
    public string Note { get; set; } = null!;
    public string? Flag { get; set; } = default;
    public Meme Meme { get; set; } = null!;
    public long UserId { get; set; }
}