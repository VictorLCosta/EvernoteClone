using EvernoteClone.Infrastructure.Persistence;
using EvernoteClone.WPF.ViewModels;

namespace EvernoteClone.WPF.Commands;

public class DeleteNotebookCommand(NotesViewModel viewModel, ApplicationDbContextFactory factory) : AsyncCommandBase
{
    public override async Task ExecuteAsync(object? parameter)
    {
        using var db = factory.CreateDbContext();

        if (viewModel.SelectedNotebook is not null)
        {
            var notebook = await db.Notebooks.FindAsync(viewModel.SelectedNotebook.Id);
            if (notebook is not null)
            {
                db.Notebooks.Remove(notebook);
                await db.SaveChangesAsync();
                viewModel.Notebooks.Remove(viewModel.SelectedNotebook);
                viewModel.SelectedNotebook = null;
            }
        }
    }
}
