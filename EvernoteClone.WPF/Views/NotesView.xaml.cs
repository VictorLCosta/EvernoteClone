using System.Windows;
using System.Windows.Documents;

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

    private void SpeechButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void ContentRichTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        int characterCount = new TextRange(
            ContentRichTextBox.Document.ContentStart, 
            ContentRichTextBox.Document.ContentEnd
        ).Text.Length;
    }

    private void BoldButton_Click(object sender, RoutedEventArgs e)
    {
        ContentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
    }
}
