using EvernoteClone.Domain.Entities;
using EvernoteClone.Infrastructure.Persistence;
using EvernoteClone.WPF.Commands;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EvernoteClone.WPF.ViewModels;

public class NotesViewModel : ViewModelBase
{
    private readonly ApplicationDbContextFactory _factory;

    public ObservableCollection<Notebook> Notebooks { get; set; } = [];
    public ObservableCollection<Note> Notes { get; set; } = [];

    private Notebook? _selectedNotebook;

    public Notebook? SelectedNotebook
    {
        get => _selectedNotebook;
        set
        {
            _selectedNotebook = value;
            OnPropertyChanged(nameof(SelectedNotebook));

            _ = GetNotes();
        }
    }

    private Note? _selectedNote;

    public Note? SelectedNote
    {
        get => _selectedNote;
        set
        {
            _selectedNote = value;
            OnPropertyChanged(nameof(SelectedNote));
        }
    }

    public ICommand NewNotebookCommand { get; set; }
    public ICommand NewNoteCommand { get; set; }
    public ICommand DeleteNotebookCommand { get; set; }

    public NotesViewModel(ApplicationDbContextFactory context)
    {
        _factory = context;

        NewNotebookCommand = new NewNotebookCommand(this, _factory);
        NewNoteCommand = new NewNoteCommand(this, _factory);
        DeleteNotebookCommand = new DeleteNotebookCommand(this, _factory);

        _ = GetNotebooks();
    }

    private async Task GetNotebooks()
    {
        using var db = _factory.CreateDbContext();

        var notebooks = await db.Notebooks.ToListAsync();

        foreach (var notebook in notebooks)
        {
            Notebooks.Add(notebook);
        }
    }

    private async Task GetNotes()
    {
        Notes.Clear();

        using var db = _factory.CreateDbContext();

        var notes = await db.Notes
            .Where(n => n.NotebookId == SelectedNotebook!.Id)
            .ToListAsync();

        foreach (var note in notes)
        {
            Notes.Add(note);
        }
    }

}
