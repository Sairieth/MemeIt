using System.Runtime.CompilerServices;
using CryptoHelper;
using MemeIt.Models.DTOs;
using MemeIt.Models.Entities;

namespace MemeIt.Infrastructure;

public static class DtoExtensions
{
    public static User ToUser(this RegisterUserDto regData)
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

    public static Meme ToMeme(this PostDataDto post, string fileUri)
    {
        return new Meme()
        {
            Title = post.Title,
            Tag = post.Tag,
            UserId = post.UserId,
            AzureUrl = fileUri
        };
    }
}