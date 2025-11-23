using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.IO;

namespace EvernoteClone.Infrastructure.Persistence;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        string connectionString = "Data Source=Data/app.db";

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
