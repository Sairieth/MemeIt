namespace MemeIt.Models.Entities;

public class User
{
    public string Name { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string Email { get; set; } = null!;
    public List<Role> Roles { get; set; } = null!;
    public List<Comment>? Comments { get; set; }
    public List<Meme>? Memes { get; set; }
}