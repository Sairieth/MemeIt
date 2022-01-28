using MemeIt.Models.Common;

namespace MemeIt.Data.Services;

public static class ConfigurationService
{
    public static Configuration Configuration { get; } = new Configuration();

    static ConfigurationService()
    {
        var configurationRoot = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        configurationRoot.Bind(Configuration);
    }
}