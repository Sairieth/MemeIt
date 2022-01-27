namespace MemeIt.Models.DTOs;

public class PostDataDto
{
    public string Title { get; set; } = null!;
    public string Tag { get; set; } = null!;
    public long UserId { get; set; }
    public IFormFile? File { get; set; }
}