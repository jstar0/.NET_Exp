using System.ComponentModel;
using CommunityToolkit.WinUI.Behaviors;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using NotePadSharp.Contracts.Services;
using NotePadSharp.Services;
using NotePadSharp.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace NotePadSharp.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();

        ViewModel.SetEditor(Editor);
        SetupDefaultFont();
    }

    private void Editor_GotFocus(object sender, RoutedEventArgs e)
    {
        Editor.Document.GetText(TextGetOptions.UseCrlf, out _);

        // reset colors to correct defaults for Focused state
        var documentRange = Editor.Document.GetRange(0, TextConstants.MaxUnitCount);
        var background = (SolidColorBrush)App.Current.Resources["TextControlBackgroundFocused"];

        if (background != null)
        {
            documentRange.CharacterFormat.BackgroundColor = background.Color;
        }
    }

    private void Editor_TextChanged(object sender, RoutedEventArgs e)
    {
        ViewModel.RichEditBoxService.PerformUndoRedoStatusChange();

        var notification = new Notification
        {
            Title = $"提示 {DateTimeOffset.Now}",
            Message = $"文字已更改",
            Severity = InfoBarSeverity.Warning,
            Duration = TimeSpan.FromSeconds(1.5)
        };
    }

    private void SetupDefaultFont()
    {
        if (Editor != null)
        {
            // 默认字体设为 Microsoft YaHei UI，字距为 10.5
            Editor.Document.Selection.CharacterFormat.Name = "Microsoft YaHei UI";
            Editor.Document.Selection.CharacterFormat.Size = 10.5f;
        }
    }

    private void Editor_OnSelectionChanged(object sender, RoutedEventArgs e)
    {
        ViewModel.RichEditBoxService.OnSelectionChanged();
    }

}
