using Evernote.ViewModel.Helpers;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace Evernote.View
{
    /// <summary>
    /// Interaction logic for NoteWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        public NotesWindow()
        {
            InitializeComponent();
            fontFamilyComboBox.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            fontSizeComboBox.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 32, 48, 72 };
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void SpeachButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var recordedText = await SpeechToTextRecorder.Record();
                contentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(recordedText)));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting speech recognition\nException message: {ex.Message}", "Operation failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ContentRichTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var totalCharacters = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd).Text.Trim().Length;
            statusTextBlock.Text = totalCharacters > 0 ? $"Document length: {totalCharacters} characters" : "";
        }

        private void ContentRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ToggleBoldButtonState();
            ToggleItalicButtonState();
            ToggleUnderlineButtonState();
            fontFamilyComboBox.SelectedItem = contentRichTextBox.Selection.GetPropertyValue(FontFamilyProperty);
            fontSizeComboBox.Text = contentRichTextBox.Selection.GetPropertyValue(FontSizeProperty).ToString();
        }

        private void ToggleUnderlineButtonState()
        {
            var textDecorations = contentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection;
            if (textDecorations != null) 
            {
                foreach (var textDecoration in textDecorations)
                {
                    if (textDecoration.Location == TextDecorationLocation.Underline)
                    {
                        underlineButton.IsChecked = true;
                        return;
                    }
                }
            }

            underlineButton.IsChecked = false;
        }

        private void ToggleItalicButtonState()
        {
            var selectedStyle = contentRichTextBox.Selection.GetPropertyValue(FontStyleProperty);
            italicButton.IsChecked = selectedStyle != null &&
                selectedStyle.ToString() == FontStyles.Italic.ToString();
        }

        private void ToggleBoldButtonState()
        {
            var selectedWeight = contentRichTextBox.Selection.GetPropertyValue(FontWeightProperty);
            boldButton.IsChecked = selectedWeight != null &&
                selectedWeight.ToString() == FontWeights.Bold.ToString();
        }

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            var isChecked = (sender as ToggleButton).IsChecked ?? false;
            if (isChecked)
            {
                contentRichTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            }
            else
            {
                contentRichTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            }
        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            var isChecked = (sender as ToggleButton).IsChecked ?? false;
            if (isChecked)
            {
                contentRichTextBox.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Italic);
            }
            else
            {
                contentRichTextBox.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Normal);
            }
        }

        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            var isChecked = (sender as ToggleButton).IsChecked ?? false;
            if (isChecked)
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
            else
            {
                (contentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection)
                    .TryRemove(TextDecorations.Underline, out var textDecorations);
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, textDecorations);
            }
        }

        private void FontFamilyComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (fontFamilyComboBox.SelectedItem is null) 
            {
                return;
            }

            contentRichTextBox.Selection.ApplyPropertyValue(FontFamilyProperty, fontFamilyComboBox.SelectedItem);
        }

        private void FontSizeComboBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (fontSizeComboBox.SelectedItem is null) 
            {
                return;
            }

            contentRichTextBox.Selection.ApplyPropertyValue(FontSizeProperty, fontSizeComboBox.SelectedItem);
        }
    }
}
