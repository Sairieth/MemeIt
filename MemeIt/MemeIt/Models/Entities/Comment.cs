using System.ComponentModel.DataAnnotations.Schema;
using MemeIt.Models.Common;
using Microsoft.AspNetCore.Components;

namespace MemeIt.Models.Entities;

public class Comment : EntityWithModificationDate
{
    public string Message { get; set; } = null!;
    //public string? Flag { get; set; } = default;
    public virtual Meme Meme { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}