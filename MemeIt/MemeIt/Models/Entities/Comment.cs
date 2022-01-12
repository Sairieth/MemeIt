using MemeIt.Models.Common;

namespace MemeIt.Models.Entities;

public class Comment:EntityWithModificationDate
{
    public string Note { get; set; } = null!;
    public string Tag { get; set; } = null!;
    public Meme Meme { get; set; } = null!;
    public User User { get; set; } = null!;
}