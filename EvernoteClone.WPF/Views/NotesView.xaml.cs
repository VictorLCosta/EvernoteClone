using EvernoteClone.WPF.ViewModels;
using System.IO;
using System.Speech.Recognition;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace EvernoteClone.WPF.Views;

public partial class NotesView : Window
{
    private readonly NotesViewModel? ViewModel;
    private SpeechRecognitionEngine recognizer = new();

    public NotesView(object dataContext)
    {
        InitializeComponent();
        InitializeSpeech();

        ViewModel = dataContext as NotesViewModel;
        DataContext = ViewModel;

        CbFontFamily.ItemsSource = Fonts.SystemFontFamilies;
        CbFontSize.ItemsSource = new List<double>() 
        { 
            8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 
        };
    }

    private void InitializeSpeech()
    {
        try
        {
            recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));

            recognizer.SetInputToDefaultAudioDevice();

            recognizer.LoadGrammar(new DictationGrammar());

            recognizer.SpeechRecognized += Recognizer_SpeechRecognized;
            recognizer.RecognizeCompleted += Recognizer_RecognizeCompleted;
        }
        catch (Exception)
        {
            var recognizers = SpeechRecognitionEngine.InstalledRecognizers();
            string msg = "Recognizers instalados:\n";

            foreach (var r in recognizers)
                msg += $"{r.Culture} - {r.Name}\n";

            MessageBox.Show(msg, "Speech Initialization Error");
        }
    }

    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {
        System.Windows.Application.Current.Shutdown();
    }

    private void SpeechButton_Click(object sender, RoutedEventArgs e)
    {
        recognizer.RecognizeAsync(RecognizeMode.Multiple);
    }

    private void Recognizer_SpeechRecognized(object? sender, SpeechRecognizedEventArgs e)
    {
        string text = e.Result.Text;

        ContentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(text)));
    }

    private void Recognizer_RecognizeCompleted(object? sender, RecognizeCompletedEventArgs e)
    {
        recognizer.RecognizeAsyncStop();
    }

    private void ContentRichTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        int characterCount = new TextRange(
            ContentRichTextBox.Document.ContentStart, 
            ContentRichTextBox.Document.ContentEnd
        ).Text.Length;

        StatusTextBlock.Text = $"Document length: {characterCount} characters";
    }

    private void BoldButton_Click(object sender, RoutedEventArgs e)
    {
        bool isChecked = (sender as ToggleButton)?.IsChecked ?? false;

        if (isChecked) 
        {
            ContentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
        }
        else
        {
            ContentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
        }
    }

    private void ContentRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
    {
        var selectedWeight = ContentRichTextBox.Selection.GetPropertyValue(Inline.FontWeightProperty);
        BoldButton.IsChecked = selectedWeight != DependencyProperty.UnsetValue && selectedWeight.Equals(FontWeights.Bold);

        CbFontFamily.SelectedItem = ContentRichTextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
        CbFontSize.Text = ContentRichTextBox.Selection.GetPropertyValue(Inline.FontSizeProperty).ToString();
    }

    private void CbFontFamily_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        ContentRichTextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, CbFontFamily.SelectedItem);
    }

    private void CbFontSize_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        ContentRichTextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, CbFontSize.SelectedItem);
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        string fileName = $"{ViewModel!.SelectedNote!.Id}.rtf";
        string rtfFile = Path.Combine(Environment.CurrentDirectory, fileName);

        using (FileStream fileStream = new(rtfFile, FileMode.Create))
        {
            TextRange range = new(ContentRichTextBox.Document.ContentStart, ContentRichTextBox.Document.ContentEnd);
            range.Save(fileStream, DataFormats.Rtf);
        }

        string fileUrl = "";
        ViewModel.SelectedNote.FileLocation = fileUrl;
    }
}
