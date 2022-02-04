using System.Runtime.CompilerServices;
using CryptoHelper;
using MemeIt.Models.DTOs;
using MemeIt.Models.Entities;

namespace MemeIt.Infrastructure;

public static class DtoExtensions
{
    public static User RegisterUserDtoToUser(this RegisterUserDto regData)
    {
        return new User()
        {
            Username = regData.UserName,
            HashedPassword = Crypto.HashPassword(regData.Password),
            Email = regData.Email,
            DateOfBirth = regData.DateOfBirth,
            Role = "baseUser",
            CreatedOn = DateTime.Now.ToLocalTime(),
            LastModified = DateTime.Now.ToLocalTime(),
            DeletedOn = DateTime.MinValue
        };
    }

    public static Meme PostDataDtoToMeme(this PostDataDto post, string fileUri,User user)
    {
        return new Meme()
        {
            Title = post.Title,
            Tag = post.Tag,
            User = user,
            AzureUrl = fileUri
        };
    }

    public static MemeDto MemeToMemeDto(this Meme meme)
    {
        return new MemeDto()
        {
            Title = meme.Title,
            Tag = meme.Tag,
            Url = meme.AzureUrl,
            UserName = meme.User.Username
        };
    }

    public static MemeCommentDto ToMemeCommentDto(this Comment comment)
    {
        return new MemeCommentDto()
        {
            Message = comment.Message,
            UserName = comment.User.Username,
            UserId = comment.User.Id
        };
    }

    public static Comment ToComment(this CommentOnMemeDto commentOnMemeDto,Meme meme,User user)
    {
        return new Comment()
        {
            Message = commentOnMemeDto.Message,
            Meme = meme,
            User = user,
            CreatedOn = DateTime.Now,
            LastModified = DateTime.Now,
            DeletedOn = DateTime.MinValue
        };
    }
}