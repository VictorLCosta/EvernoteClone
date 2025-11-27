using EvernoteClone.Application.Common.Interfaces;
using EvernoteClone.Domain.Entities;
using EvernoteClone.WPF.ViewModels;

namespace EvernoteClone.WPF.Commands;

public class NewNoteCommand(NotesViewModel viewModel, IApplicationDbContext context) : AsyncCommandBase
{

    public override bool CanExecute(object? parameter)
    {
        return parameter is Notebook && parameter != null;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        if (parameter is not Notebook notebook)
            return;

        var note = new Note()
        {
            Title = "New Note",
            NotebookId = notebook.Id
        };

        await context.Notes.AddAsync(note);
        await context.SaveChangesAsync(CancellationToken.None);

        viewModel.Notes.Add(note);
    }
}
