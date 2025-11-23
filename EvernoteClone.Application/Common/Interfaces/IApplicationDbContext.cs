using EvernoteClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvernoteClone.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Note> Notes { get; }
    DbSet<Notebook> Notebooks { get; }
    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
