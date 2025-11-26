using EvernoteClone.Infrastructure;
using EvernoteClone.Infrastructure.Persistence;
using EvernoteClone.WPF.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace EvernoteClone.WPF;

public partial class App : System.Windows.Application
{
    private readonly IHost _host;

    public App()
    {
        _host = CreateHostBuilder().Build();
    }

    public static IHostBuilder CreateHostBuilder(string[]? args = null) =>
        Host.CreateDefaultBuilder(args)
            .AddInfrastructure()
            .AddViewModels()
            .AddViews();

    protected override void OnStartup(StartupEventArgs e)
    {
        _host.Start();

        var initialiser = _host.Services.GetRequiredService<ApplicationDbContextInitialiser>();
        Task.Run(async () => await initialiser.InitialiseAsync(new CancellationToken()));

        Window window = _host.Services.GetRequiredService<NotesView>();
        window.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _host.StopAsync();
        _host.Dispose();

        base.OnExit(e);
    }
}
