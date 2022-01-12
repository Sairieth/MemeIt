using MemeIt.Models.Common;

namespace MemeIt.Models.Entities;

public class Meme:EntityWithModificationDate
{
    public string Title { get; set; } = null!;
    public string Tag { get; set; } = null!;
    public string AzureUrl { get; set; } = null!;
    public List<Comment>? Comments { get; set; }
}