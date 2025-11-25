using System.Windows;

namespace EvernoteClone.WPF.Views;

public partial class NotesView : Window
{
    public NotesView(object dataContext)
    {
        InitializeComponent();

        DataContext = dataContext;
    }

    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {
        System.Windows.Application.Current.Shutdown();
    }
}
