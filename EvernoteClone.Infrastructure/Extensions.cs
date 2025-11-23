using EvernoteClone.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace EvernoteClone.Infrastructure;

public static class Extensions
{
    public static IHostBuilder AddInfrastructure(this IHostBuilder host) => host
        .AddConfiguration()
        .AddPersistence();

    private static IHostBuilder AddConfiguration(this IHostBuilder host)
    {
        host.ConfigureAppConfiguration(c =>
        {
            c.AddJsonFile("appsettings.json");
            c.AddEnvironmentVariables();
        });

        return host;
    }
}
