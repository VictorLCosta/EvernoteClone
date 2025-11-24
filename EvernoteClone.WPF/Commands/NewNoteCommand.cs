using EvernoteClone.WPF.ViewModels;

namespace EvernoteClone.WPF.Commands;

public class NewNoteCommand(NotesViewModel viewModel) : AsyncCommandBase
{
    public override Task ExecuteAsync(object? parameter)
    {
        throw new NotImplementedException();
    }
}
