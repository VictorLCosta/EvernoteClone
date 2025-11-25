using EvernoteClone.WPF.ViewModels;
using EvernoteClone.WPF.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EvernoteClone.WPF;

internal static class Extensions
{
    internal static IHostBuilder AddViewModels(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            services.AddTransient<LoginViewModel>();
            services.AddTransient<NotesViewModel>();
        });

        return host;
    }

    internal static IHostBuilder AddViews(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            services.AddSingleton(s => new NotesView(s.GetRequiredService<NotesViewModel>()));
        });

        return host;
    }
}
