using MemeIt.Models.Common;

namespace MemeIt.Models.Entities;

public class Role : EntityWithModificationDate
{
    public string? Title { get; set; }
}