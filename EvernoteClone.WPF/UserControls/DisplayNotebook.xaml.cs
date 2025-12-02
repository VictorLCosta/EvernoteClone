using EvernoteClone.Domain.Entities;
using System.Windows;
using System.Windows.Controls;

namespace EvernoteClone.WPF.UserControls;

public partial class DisplayNotebook : UserControl
{
    public Notebook Notebook 
    {
        get => (Notebook)GetValue(NotebookProperty); 
        set => SetValue(NotebookProperty, value); 
    }

    public static readonly DependencyProperty NotebookProperty =
        DependencyProperty.Register(
            nameof(Notebook),
            typeof(Notebook),
            typeof(DisplayNotebook),
            new PropertyMetadata(null, SetValues));

    private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is DisplayNotebook control)
        {
            control.DataContext = control.Notebook;
        }
    }

    public DisplayNotebook()
    {
        InitializeComponent();
    }
}
