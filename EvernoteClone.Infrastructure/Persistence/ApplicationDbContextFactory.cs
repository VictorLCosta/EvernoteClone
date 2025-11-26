using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;

namespace EvernoteClone.Infrastructure.Persistence;

internal class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();
        var dbPath = Path.Combine(basePath, "Persistence", "Data", "app.db");

        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        builder.UseSqlite($"Data Source={dbPath}");

        return new ApplicationDbContext(builder.Options);
    }
}
