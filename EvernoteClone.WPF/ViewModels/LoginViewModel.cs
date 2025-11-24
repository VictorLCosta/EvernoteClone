using EvernoteClone.WPF.Commands;
using System.Windows.Input;

namespace EvernoteClone.WPF.ViewModels;

public class LoginViewModel : ViewModelBase
{
    public ICommand LoginCommand { get; set; }

    public LoginViewModel()
    {
        LoginCommand = new LoginCommand(this);
    }
}
