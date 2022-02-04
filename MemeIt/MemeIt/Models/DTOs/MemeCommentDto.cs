namespace MemeIt.Models.DTOs;

public class MemeCommentDto
{
    public string Message { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public long UserId { get; set; }
}