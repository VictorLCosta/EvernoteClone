using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EvernoteClone.Infrastructure.Persistence;

public static class Extensions
{
    public static IHostBuilder AddPersistence(this IHostBuilder host)
    {
        host.ConfigureServices((context, services) =>
        {
            var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
            Action<DbContextOptionsBuilder> configureDbContext = o => o.UseSqlite(connectionString).EnableSensitiveDataLogging();

            services.AddDbContext<ApplicationDbContext>(configureDbContext);

            services
                .AddTransient<ApplicationDbContextInitialiser>()
                .AddSingleton<ApplicationDbContextFactory>(new ApplicationDbContextFactory(configureDbContext));
        });

        return host;
    }
}
