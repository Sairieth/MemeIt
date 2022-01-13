using MemeIt.Models.Common;

namespace MemeIt.Models.Entities;

public class Tag : Entity
{
    public string Title { get; set; } = null!;
}