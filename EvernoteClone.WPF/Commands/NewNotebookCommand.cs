using EvernoteClone.Application.Common.Interfaces;
using EvernoteClone.Domain.Entities;
using EvernoteClone.WPF.ViewModels;

namespace EvernoteClone.WPF.Commands;

public class NewNotebookCommand(NotesViewModel viewModel, IApplicationDbContext context) : AsyncCommandBase
{
    public override async Task ExecuteAsync(object? parameter)
    {
        var notebook = new Notebook()
        {
            Name = "New Notebook"
        };

        await context.Notebooks.AddAsync(notebook);
        await context.SaveChangesAsync(CancellationToken.None);

        viewModel.Notebooks.Add(notebook);
    }
}
