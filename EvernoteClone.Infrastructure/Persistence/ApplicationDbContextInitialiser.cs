using Microsoft.EntityFrameworkCore;

namespace EvernoteClone.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser(ApplicationDbContext context)
{
    public async Task InitialiseAsync(CancellationToken cancellationToken)
    {
        try
        {
            if (context.Database.GetMigrations().Any())
            {
                if ((await context.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
                {
                    await context.Database.MigrateAsync(cancellationToken);
                }
                if (await context.Database.CanConnectAsync(cancellationToken))
                {
                    await SeedAsync(cancellationToken);
                }
            }
        }
        catch (Exception)
        {         
            throw;
        }
    }

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        try
        {
            await TrySeedAsync(cancellationToken);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task TrySeedAsync(CancellationToken cancellationToken)
    {
        if (!await context.Users.AnyAsync(cancellationToken))
        {
            var user = new Domain.Entities.User
            {
                UserName = "admin",
                Name = "Admin",
                LastName = "User",
                Password = "Admin@123"
            };

            await context.Users.AddAsync(user, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!await context.Notebooks.AnyAsync(cancellationToken))
        {
            var notebooks = new List<Domain.Entities.Notebook>
            {
                new() {
                    Name = "Personal",
                    UserId = 1
                },
                new() {
                    Name = "Work",
                    UserId = 1
                }
            };

            await context.Notebooks.AddRangeAsync(notebooks, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
