using EvernoteClone.Application.Common.Interfaces;
using EvernoteClone.Domain.Entities;
using EvernoteClone.WPF.Commands;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EvernoteClone.WPF.ViewModels;

public class NotesViewModel : ViewModelBase
{
    private readonly IApplicationDbContext _context;

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

    public ICommand NewNotebookCommand { get; set; }
    public ICommand NewNoteCommand { get; set; }

    public NotesViewModel(IApplicationDbContext context)
    {
        _context = context;

        NewNotebookCommand = new NewNotebookCommand(this, _context);
        NewNoteCommand = new NewNoteCommand(this, _context);

        _ = GetNotebooks();
    }

    private async Task GetNotebooks()
    {
        var notebooks = await _context.Notebooks.ToListAsync();

        foreach (var notebook in notebooks)
        {
            Notebooks.Add(notebook);
        }
    }

    private async Task GetNotes()
    {
        Notes.Clear();

        var notes = await _context.Notes
            .Where(n => n.NotebookId == SelectedNotebook!.Id)
            .ToListAsync();

        foreach (var note in notes)
        {
            Notes.Add(note);
        }
    }

}
