using CommunityToolkit.WinUI.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;

using NotePadSharp.Contracts.Services;
using NotePadSharp.Helpers;
using NotePadSharp.Services;
using NotePadSharp.ViewModels;

using Windows.System;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml.Navigation;

namespace NotePadSharp.Views;

// TODO: Update NavigationViewItem titles and icons in ShellPage.xaml.
public sealed partial class ShellPage : Page
{
    public ShellViewModel ViewModel
    {
        get;
    }

    private readonly IRichEditBoxService _richEditBoxService;

    public bool ContextualItem => true;

    public ShellPage(ShellViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();

        this.NavigationFrame.Navigated += OnNavigated;
        ViewModel.NavigationService.Frame = NavigationFrame;
        ViewModel.NavigationViewService.Initialize(TabbedCommandBar);
        //ViewModel.NavigationViewService.Initialize(NavigationViewControl);

        // TODO: Set the title bar icon by updating /Assets/WindowIcon.ico.
        // A custom title bar is required for full window theme and Mica support.
        // https://docs.microsoft.com/windows/apps/develop/title-bar?tabs=winui3#full-customization
        App.MainWindow.ExtendsContentIntoTitleBar = true;
        App.MainWindow.SetTitleBar(AppTitleBar);
        App.MainWindow.Activated += MainWindow_Activated;
        AppTitleBarText.Text = "AppDisplayName".GetLocalized();

        var mainNotificationService = App.GetService<IMainNotificationService>();
        mainNotificationService.SetNotificationQueue(NotificationQueue);

        _richEditBoxService = App.GetService<IRichEditBoxService>();

        _richEditBoxService.UndoRedoStateChanged += RichEditBoxService_UndoRedoStateChanged;
        _richEditBoxService.SelectionChanged += RichEditBoxService_SelectionChanged;
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        if (e.SourcePageType == typeof(SettingsPage))
        {
            TabbedCommandBarItemEnabled(false);
            return;
        }

        TabbedCommandBarItemEnabled(true);
    }

    private void TabbedCommandBarItemEnabled(bool b)
    {
        TabbedCommandBarHome.IsEnabled = b;
        TabbedCommandBarInsert.IsEnabled = b;
        PictureFormat.IsEnabled = b;
    }

    private void OnLoaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        TitleBarHelper.UpdateTitleBar(RequestedTheme);

        KeyboardAccelerators.Add(BuildKeyboardAccelerator(VirtualKey.Left, VirtualKeyModifiers.Menu));
        KeyboardAccelerators.Add(BuildKeyboardAccelerator(VirtualKey.GoBack));
    }


    private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
    {
        App.AppTitlebar = AppTitleBarText as UIElement;
    }

    private static KeyboardAccelerator BuildKeyboardAccelerator(VirtualKey key, VirtualKeyModifiers? modifiers = null)
    {
        var keyboardAccelerator = new KeyboardAccelerator() { Key = key };

        if (modifiers.HasValue)
        {
            keyboardAccelerator.Modifiers = modifiers.Value;
        }

        keyboardAccelerator.Invoked += OnKeyboardAcceleratorInvoked;

        return keyboardAccelerator;
    }

    private static void OnKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        var navigationService = App.GetService<INavigationService>();

        var result = navigationService.GoBack();

        args.Handled = result;
    }

    private void RichEditBoxService_UndoRedoStateChanged(object? sender, EventArgs e)
    {
        UndoButton.IsEnabled = _richEditBoxService.CanUndo;
        RedoButton.IsEnabled = _richEditBoxService.CanRedo;
    }

    private void RichEditBoxService_SelectionChanged(object? sender, EventArgs e)
    {
        /*ColorPickerButton.SelectedColor = ;
        FontComboBox.SelectedItem = ;
        FontSizeTxtBox.SelectedText = ;
        BoldToggleButton.IsChecked = ;
        ItalicToggleButton.IsChecked = ;
        UnderlineToggleButton = ;
        StrikethroughToggleButton = ;
        AlignLeftToggleButton = ;
        AlignCenterToggleButton = ;
        AlignRightToggleButton = ;*/

        _richEditBoxService.IsUpdatingSelection = true;

        try
        {
            var selection = _richEditBoxService.Editor.Document.Selection;
            var charFormat = selection.CharacterFormat;

            // 更新字体颜色
            ColorPickerButton.SelectedColor = charFormat.ForegroundColor;

            // 更新字体
            if (!string.IsNullOrEmpty(charFormat.Name))
            {
                var fontFamily = FontComboBox.Items
                    .Cast<FontFamily>()
                    .FirstOrDefault(x => x.Source == charFormat.Name);
                if (fontFamily != null)
                {
                    FontComboBox.SelectedItem = fontFamily;
                }
            }

            // 更新字体大小
            if (charFormat.Size > 0)
            {
                FontSizeTxtBox.Text = charFormat.Size.ToString();
            }

            // 更新字体样式
            BoldToggleButton.IsChecked = charFormat.Bold == FormatEffect.On;
            ItalicToggleButton.IsChecked = charFormat.Italic == FormatEffect.On;
            UnderlineToggleButton.IsChecked = charFormat.Underline != UnderlineType.None;
            StrikethroughToggleButton.IsChecked = charFormat.Strikethrough == FormatEffect.On;

            // 更新对齐方式
            var paraFormat = selection.ParagraphFormat;
            AlignLeftToggleButton.IsChecked = paraFormat.Alignment == ParagraphAlignment.Left;
            AlignCenterToggleButton.IsChecked = paraFormat.Alignment == ParagraphAlignment.Center;
            AlignRightToggleButton.IsChecked = paraFormat.Alignment == ParagraphAlignment.Right;
        }
        finally
        {
            _richEditBoxService.IsUpdatingSelection = false;
        }

    }

    private void FontComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_richEditBoxService.IsUpdatingSelection)
        {
            return;
        }

        if (sender is ComboBox { SelectedItem: Microsoft.UI.Xaml.Media.FontFamily selectedFont })
        {
            _richEditBoxService.Editor.Document.Selection.CharacterFormat.Name = selectedFont.Source;
        }
    }


    private void FontSizeTxtBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (_richEditBoxService.IsUpdatingSelection)
        {
            return;
        }

        if (sender is TextBox { Text: string fontSizeText })
        {
            if (double.TryParse(fontSizeText, out var fontSize))
            {
                _richEditBoxService.Editor.Document.Selection.CharacterFormat.Size = (float)fontSize;
            }
        }
    }
}
