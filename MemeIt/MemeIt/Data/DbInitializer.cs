using MemeIt.Core;
using MemeIt.Data.Services;
using MemeIt.Models.Entities;

namespace MemeIt.Data;

public static class DbInitializer
{
    private static void Initialize(AppDbContext context, IAuthService service)
    {
        context.Database.EnsureCreated();

        // Look for any Entity.
        if (context.Users.Any())
        {
            return; // DB has been seeded
        }

        var roles = new Role[]
        {
            new Role()
            {
                Title = "baseUser",
                CreatedOn = DateTime.Now.ToUniversalTime(),
                LastModified = DateTime.Now.ToUniversalTime(),
                DeletedOn = DateTime.MinValue
            }
        };
        foreach (var role in roles)
        {
            context.Roles.Add(role);
        }

        context.SaveChanges();

        var user = new User()
        {
            Username = "OG",
            HashedPassword = service.HashPassword("Bob"),
            DateOfBirth = DateTime.Parse("2.6.1945"),
            Email = "i@mbobmarley.com",
            Role = roles.First().Title!,
            CreatedOn = DateTime.Now.ToUniversalTime(),
            LastModified = DateTime.Now.ToUniversalTime(),
            DeletedOn = DateTime.MinValue
        };
        context.Users.Add(user);
        context.SaveChanges();
    }

    public static void CreateDbIfNotExists(IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<AppDbContext>();
                var authService = services.GetRequiredService<IAuthService>();
                DbInitializer.Initialize(context, authService);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred creating the DB.");
            }
        }
    }
}