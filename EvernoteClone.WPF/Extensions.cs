using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EvernoteClone.WPF;

internal static class Extensions
{
    internal static IHostBuilder AddViews(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            services.AddSingleton(s => new MainWindow());
        });

        return host;
    }
}
