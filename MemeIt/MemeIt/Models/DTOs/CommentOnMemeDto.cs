namespace MemeIt.Models.DTOs;

public class CommentOnMemeDto
{
    public string Message { get; set; } = null!;
    public long UserId { get; set; }
    public long MemeId { get; set; }
}