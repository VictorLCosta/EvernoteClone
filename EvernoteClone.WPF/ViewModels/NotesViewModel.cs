using EvernoteClone.Application.Common.Interfaces;
using EvernoteClone.Domain.Entities;
using EvernoteClone.WPF.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EvernoteClone.WPF.ViewModels;

public class NotesViewModel : ViewModelBase
{
    public ObservableCollection<Notebook> Notebooks { get; set; } = [];

    private Notebook? _selectedNotebook;

    public Notebook? SelectedNotebook
    {
        get => _selectedNotebook;
        set
        {
            _selectedNotebook = value;
            OnPropertyChanged(nameof(SelectedNotebook));
        }
    }

    public ICommand NewNotebookCommand { get; set; }
    public ICommand NewNoteCommand { get; set; }

    public NotesViewModel(IApplicationDbContext context)
    {
        NewNotebookCommand = new NewNotebookCommand(this, context);
        NewNoteCommand = new NewNoteCommand(this);
    }
}
