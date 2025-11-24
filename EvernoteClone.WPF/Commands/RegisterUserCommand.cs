using EvernoteClone.WPF.ViewModels;

namespace EvernoteClone.WPF.Commands;

public class RegisterUserCommand(LoginViewModel viewModel) : AsyncCommandBase
{
    public override Task ExecuteAsync(object? parameter)
    {
        throw new NotImplementedException();
    }
}
