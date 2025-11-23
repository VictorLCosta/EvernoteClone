using EvernoteClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvernoteClone.Infrastructure.Persistence.Configurations;

internal class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder
            .HasIndex(x => x.Title);

        builder
            .HasIndex(x => x.CreatedAt);
    }
}

internal class NotebookConfiguration : IEntityTypeConfiguration<Notebook>
{
    public void Configure(EntityTypeBuilder<Notebook> builder)
    {
        builder
            .HasMany(x => x.Notes)
            .WithOne(x => x.Notebook)
            .HasForeignKey(x => x.NotebookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasMany(x => x.Notebooks)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
