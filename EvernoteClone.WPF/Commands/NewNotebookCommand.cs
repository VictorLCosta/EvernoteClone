using EvernoteClone.Domain.Entities;
using EvernoteClone.Infrastructure.Persistence;
using EvernoteClone.WPF.ViewModels;

namespace EvernoteClone.WPF.Commands;

public class NewNotebookCommand(NotesViewModel viewModel, ApplicationDbContextFactory factory) : AsyncCommandBase
{
    public override async Task ExecuteAsync(object? parameter)
    {
        using var db = factory.CreateDbContext();

        var notebook = new Notebook()
        {
            Name = "New Notebook",
            UserId = 1
        };

        await db.Notebooks.AddAsync(notebook);
        await db.SaveChangesAsync(CancellationToken.None);

        viewModel.Notebooks.Add(notebook);
    }
}
