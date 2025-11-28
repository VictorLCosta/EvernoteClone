using System.Speech.Recognition;
using System.Windows;
using System.Windows.Documents;
using static System.Net.Mime.MediaTypeNames;

namespace EvernoteClone.WPF.Views;

public partial class NotesView : Window
{
    private SpeechRecognitionEngine recognizer = new();

    public NotesView(object dataContext)
    {
        InitializeComponent();
        InitializeSpeech();

        DataContext = dataContext;
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
        ContentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
    }
}
