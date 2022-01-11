using MemeIt.Data.Serices;

namespace MemeIt.Data;

public static class DbInitializer
{
    private static void Initialize(AppDbContext context, IAuthService service)
    {
        context.Database.EnsureCreated();

        // Look for any Entity.
        //    if (context.entity.Any())
        //    {
        //        return; // DB has been seeded
        //    }
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