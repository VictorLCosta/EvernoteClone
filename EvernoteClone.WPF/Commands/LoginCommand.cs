using EvernoteClone.WPF.ViewModels;

namespace EvernoteClone.WPF.Commands;

public class LoginCommand(LoginViewModel viewModel) : AsyncCommandBase
{
    public override Task ExecuteAsync(object? parameter)
    {
        throw new NotImplementedException();
    }
}
