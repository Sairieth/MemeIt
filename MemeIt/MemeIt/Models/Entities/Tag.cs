using MemeIt.Models.Common;

namespace MemeIt.Models.Entities;

public class Tag : EntityWithModificationDate
{
    public string Title { get; set; } = null!;
}