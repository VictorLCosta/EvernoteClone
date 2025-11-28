using EvernoteClone.Domain.Entities;
using EvernoteClone.Infrastructure.Persistence;
using EvernoteClone.WPF.ViewModels;

namespace EvernoteClone.WPF.Commands;

public class NewNoteCommand(NotesViewModel viewModel, ApplicationDbContextFactory factory) : AsyncCommandBase
{

    public override bool CanExecute(object? parameter)
    {
        return parameter is Notebook && parameter != null;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        if (parameter is not Notebook notebook)
            return;

        using var db = factory.CreateDbContext();

        var note = new Note()
        {
            Title = "New Note",
            NotebookId = notebook.Id
        };

        await db.Notes.AddAsync(note);
        await db.SaveChangesAsync(CancellationToken.None);

        viewModel.Notes.Add(note);
    }
}
