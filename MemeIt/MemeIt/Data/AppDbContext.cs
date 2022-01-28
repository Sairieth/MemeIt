using System.Runtime.CompilerServices;
using MemeIt.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MemeIt.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Comment> Comments { get; set; } = default!;
    public DbSet<Meme> Memes { get; set; } = default!;
    public DbSet<Role> Roles { get; set; } = default!;
    public DbSet<Tag> Tags { get; set; } = default!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("User");
        modelBuilder.Entity<Comment>(prop =>
        {
            prop.HasOne(c => c.Meme)
                .WithMany(m => m.Comments)
                .HasForeignKey(c => c.Id)
                .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
            prop.HasOne(c => c.User)
                .WithMany(m => m.Comments)
                .HasForeignKey(c => c.Id)
                .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
            prop.ToTable("Comment");
        });

        modelBuilder.Entity<Meme>(prop =>
        {
            prop.HasOne(m => m.User)
                .WithMany(u => u.Memes)
                .HasForeignKey(m => m.Id)
                .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
            prop.ToTable("Meme");
        });

        modelBuilder.Entity<Role>().ToTable("Role");
        modelBuilder.Entity<Tag>().ToTable("Tag");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"))
                .LogTo(Console.WriteLine
                    , new[] {DbLoggerCategory.Database.Command.Name}
                    , Microsoft.Extensions.Logging.LogLevel.Information);
        }
    }
}