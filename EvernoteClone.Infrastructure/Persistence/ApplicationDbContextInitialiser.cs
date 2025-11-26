using Microsoft.EntityFrameworkCore;

namespace EvernoteClone.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser(ApplicationDbContext context)
{
    public async Task InitialiseAsync(CancellationToken cancellationToken)
    {
        try
        {
            var pending = await context.Database.GetPendingMigrationsAsync(cancellationToken);

            if (pending.Any())
            {
                await context.Database.MigrateAsync(cancellationToken);
            }

            if (await context.Database.CanConnectAsync(cancellationToken))
            {
                await SeedAsync(cancellationToken);
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
        await context.SaveChangesAsync(cancellationToken);
    }
}
