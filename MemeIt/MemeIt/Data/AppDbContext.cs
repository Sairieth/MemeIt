using MemeIt.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MemeIt.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public AppDbContext()
    {
    }

    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Comment> Comments { get; set; } = default!;
    public DbSet<Meme> Memes { get; set; } = default!;
    public DbSet<Role> Roles { get; set; } = default!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("User");
        modelBuilder.Entity<Comment>().ToTable("Comment");
        modelBuilder.Entity<Meme>().ToTable("Meme");
        modelBuilder.Entity<Role>().ToTable("Role");
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