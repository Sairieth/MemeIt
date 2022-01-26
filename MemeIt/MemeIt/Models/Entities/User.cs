using MemeIt.Models.Common;

namespace MemeIt.Models.Entities;

public class User : EntityWithModificationDate
{
    public string Username { get; set; } = null!;
    public string HashedPassword { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; } = null!;
    public Role Role { get; set; } = null!;
    public List<Comment>? Comments { get; set; }
    public List<Meme>? Memes { get; set; }
}