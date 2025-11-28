using Microsoft.EntityFrameworkCore;

namespace EvernoteClone.Infrastructure.Persistence;

public class ApplicationDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
{
    public ApplicationDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<ApplicationDbContext> options = new();

        configureDbContext(options);

        return new ApplicationDbContext(options.Options);
    }
}
