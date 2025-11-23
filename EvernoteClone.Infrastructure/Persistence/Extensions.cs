using EvernoteClone.Application.Common.Interfaces;
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

            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseSqlite(connectionString).EnableSensitiveDataLogging();
            });

            services.AddTransient<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
        });

        return host;
    }
}
