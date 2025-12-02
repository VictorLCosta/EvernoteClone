using EvernoteClone.Domain.Entities;
using System.Windows;
using System.Windows.Controls;

namespace EvernoteClone.WPF.UserControls;

public partial class DisplayNote : UserControl
{
    public Note Note
    {
        get => (Note)GetValue(NoteProperty);
        set => SetValue(NoteProperty, value);
    }

    public static readonly DependencyProperty NoteProperty =
        DependencyProperty.Register(
            nameof(Note),
            typeof(Note),
            typeof(DisplayNote),
            new PropertyMetadata(null, SetValues));

    private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is DisplayNote control)
        {
            control.DataContext = control.Note;
        }
    }

    public DisplayNote()
    {
        InitializeComponent();
    }
}
