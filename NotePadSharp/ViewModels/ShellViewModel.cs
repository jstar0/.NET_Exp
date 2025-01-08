using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.UI;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.WinUI.Behaviors;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml.Navigation;

using NotePadSharp.Contracts.Services;
using NotePadSharp.Views;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using NotePadSharp.Services;

namespace NotePadSharp.ViewModels;

public partial class ShellViewModel : ObservableRecipient
{
    [ObservableProperty]
    private bool isBackEnabled;

    [ObservableProperty]
    private object? selected;

    public INavigationService NavigationService
    {
        get;
    }

    public INavigationViewService NavigationViewService
    {
        get;
    }

    public IRichEditBoxService RichEditBoxService
    {
        get;
    }

    private readonly IMainNotificationService _mainNotificationService;

    public List<FontFamily> SystemFonts { get; private set; }

    public ICommand OnFileOpen => new AsyncRelayCommand(FileOpen);
    public ICommand OnFileSave => new AsyncRelayCommand(FileSave);

    public ICommand OnNewFile => new AsyncRelayCommand(NewFile);

    public ICommand OnShare => new AsyncRelayCommand(Share);

    public ICommand OnUndo => new AsyncRelayCommand(Undo);

    private Task Undo()
    {
        if (RichEditBoxService.CanUndo)
        {
            RichEditBoxService.Undo();
        }
        return Task.CompletedTask;
    }

    public ICommand OnRedo => new AsyncRelayCommand(Redo);

    private Task Redo()
    {
        if (RichEditBoxService.CanRedo)
        {
            RichEditBoxService.Redo();
        }
        return Task.CompletedTask;
    }

    public ICommand OnPaste => new AsyncRelayCommand(Paste);

    private Color _selectedColor;
    public Color SelectedColor
    {
        get => _selectedColor;
        set
        {
            if (_selectedColor != value)
            {
                _selectedColor = value;
                OnPropertyChanged(nameof(SelectedColor));

                if (RichEditBoxService.IsUpdatingSelection)
                {
                    return;
                }

                // 更改选中部分的颜色为选中的颜色
                RichEditBoxService.Editor.Document.Selection.CharacterFormat.ForegroundColor = _selectedColor;
            }
        }
    }

    private bool _isBold;
    public bool IsBold
    {
        get => _isBold;
        set
        {
            if (_isBold != value)
            {
                _isBold = value;
                OnPropertyChanged(nameof(IsBold));

                if (RichEditBoxService.IsUpdatingSelection)
                {
                    return;
                }

                RichEditBoxService.Editor.Document.Selection.CharacterFormat.Bold = _isBold ? FormatEffect.On : FormatEffect.Off;
            }
        }
    }

    private bool _isItalic;
    public bool IsItalic
    {
        get => _isItalic;
        set
        {
            if (_isItalic != value)
            {
                _isItalic = value;
                OnPropertyChanged(nameof(IsItalic));

                if (RichEditBoxService.IsUpdatingSelection)
                {
                    return;
                }

                RichEditBoxService.Editor.Document.Selection.CharacterFormat.Italic = _isItalic ? FormatEffect.On : FormatEffect.Off;
            }
        }
    }

    private bool _isUnderline;
    public bool IsUnderline
    {
        get => _isUnderline;
        set
        {
            if (_isUnderline != value)
            {
                _isUnderline = value;
                OnPropertyChanged(nameof(IsUnderline));

                if (RichEditBoxService.IsUpdatingSelection)
                {
                    return;
                }

                RichEditBoxService.Editor.Document.Selection.CharacterFormat.Underline = _isUnderline ? UnderlineType.Single : UnderlineType.None;
            }
        }
    }

    private bool _isStrikethrough;

    public bool IsStrikethrough
    {
        get => _isStrikethrough;
        set
        {
            if (_isStrikethrough != value)
            {
                _isStrikethrough = value;
                OnPropertyChanged(nameof(IsStrikethrough));

                if (RichEditBoxService.IsUpdatingSelection)
                {
                    return;
                }

                RichEditBoxService.Editor.Document.Selection.CharacterFormat.Strikethrough = _isStrikethrough ? FormatEffect.On : FormatEffect.Off;
            }
        }
    }

    private bool _isAlignLeft;
    public bool IsAlignLeft
    {
        get => _isAlignLeft;
        set
        {
            if (_isAlignLeft != value)
            {
                _isAlignLeft = value;
                OnPropertyChanged(nameof(IsAlignLeft));
                if (RichEditBoxService.IsUpdatingSelection)
                {
                    return;
                }
                RichEditBoxService.Editor.Document.Selection.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                RichEditBoxService.IsUpdatingSelection = true;
                IsAlignCenter = false;
                IsAlignRight = false;
                RichEditBoxService.IsUpdatingSelection = false;
            }
        }
    }

    private bool _isAlignCenter;

    public bool IsAlignCenter
    {
        get => _isAlignCenter;
        set
        {
            if (_isAlignCenter != value)
            {
                _isAlignCenter = value;
                OnPropertyChanged(nameof(IsAlignCenter));
                if (RichEditBoxService.IsUpdatingSelection)
                {
                    return;
                }

                RichEditBoxService.Editor.Document.Selection.ParagraphFormat.Alignment = ParagraphAlignment.Center;

                RichEditBoxService.IsUpdatingSelection = true;
                IsAlignLeft = false;
                IsAlignRight = false;
                RichEditBoxService.IsUpdatingSelection = false;
            }
        }
    }

    private bool _isAlignRight;
    public bool IsAlignRight
    {
        get => _isAlignRight;
        set
        {
            if (_isAlignRight != value)
            {
                _isAlignRight = value;
                OnPropertyChanged(nameof(IsAlignRight));
                if (RichEditBoxService.IsUpdatingSelection)
                {
                    return;
                }

                RichEditBoxService.Editor.Document.Selection.ParagraphFormat.Alignment = ParagraphAlignment.Right;

                RichEditBoxService.IsUpdatingSelection = true;
                IsAlignLeft = false;
                IsAlignCenter = false;
                RichEditBoxService.IsUpdatingSelection = false;

            }
        }
    }

    private Task Paste()
    {
        RichEditBoxService.Paste();
        return Task.CompletedTask;
    }

    private Task Share()
    {
        var notification = new Notification
        {
            Title = $"提示 {DateTimeOffset.Now}",
            Message = $"分享功能暂不可用。",
            Severity = InfoBarSeverity.Informational,
            Duration = TimeSpan.FromSeconds(2)
        };

        _mainNotificationService.ShowNotification(notification);

        return Task.CompletedTask;
    }

    public ShellViewModel(INavigationService navigationService, INavigationViewService navigationViewService, IRichEditBoxService richEditBoxService, IMainNotificationService mainNotificationService)
    {
        NavigationService = navigationService;
        NavigationService.Navigated += OnNavigated;
        NavigationViewService = navigationViewService;
        RichEditBoxService = richEditBoxService;
        _mainNotificationService = mainNotificationService;

        SystemFonts = new List<FontFamily>();
        LoadSystemFonts();
    }

    private async Task FileSave()
    {
        try
        {
            var savePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };

            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("Rich Text", new List<string>() { ".rtf" });

            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "New Document";

            // Initialize the picker with the window handle of the main window.
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(savePicker, hwnd);

            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                // Prevent updates to the remote version of the file until we
                // finish making changes and call CompleteUpdatesAsync.
                CachedFileManager.DeferUpdates(file);
                // write to file
                using var randAccStream =
                    await file.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);
                RichEditBoxService.SaveRtfContent(randAccStream);

                // Let Windows know that we're finished changing the file so the
                // other app can update the remote version of the file.
                var status = await CachedFileManager.CompleteUpdatesAsync(file);
                if (status != FileUpdateStatus.Complete)
                {
                    var notification = new Notification
                    {
                        Title = $"提示 {DateTimeOffset.Now}",
                        Message = $"文件 {file.Name} 保存出错",
                        Severity = InfoBarSeverity.Error,
                        Duration = TimeSpan.FromSeconds(10)
                    };

                    _mainNotificationService.ShowNotification(notification);
                    return;
                }

                var notificationSuccess = new Notification
                {
                    Title = $"提示 {DateTimeOffset.Now}",
                    Message = $"文件已成功保存。\n路径 {file.Path}",
                    Severity = InfoBarSeverity.Success,
                    Duration = TimeSpan.FromSeconds(3)
                };

                _mainNotificationService.ShowNotification(notificationSuccess);
            }
        }
        catch (Exception ex)
        {
            var notification = new Notification
            {
                Title = $"提示 {DateTimeOffset.Now}",
                Message = $"文件保存出错：\n{ex.Message}",
                Severity = InfoBarSeverity.Error,
                Duration = TimeSpan.FromSeconds(10)
            };

            _mainNotificationService.ShowNotification(notification);
        }
    }

    private async Task FileOpen()
    {
        try
        {
            // Open a text file.
            var open =
                new Windows.Storage.Pickers.FileOpenPicker
                {
                    SuggestedStartLocation =
                        Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary
                };
            open.FileTypeFilter.Add(".rtf");

            // Initialize the picker with the window handle of the main window.
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(open, hwnd);

            var file = await open.PickSingleFileAsync();

            if (file != null)
            {
                using var randAccStream =
                    await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                // Load the file into the Document property of the RichEditBox.
                RichEditBoxService.LoadRtfContent(randAccStream);

                var notification = new Notification
                {
                    Title = $"提示 {DateTimeOffset.Now}",
                    Message = $"文件已成功载入",
                    Severity = InfoBarSeverity.Success,
                    Duration = TimeSpan.FromSeconds(3)
                };

                _mainNotificationService.ShowNotification(notification);
            }
        }
        catch (Exception ex)
        {
            var notification = new Notification
            {
                Title = $"提示 {DateTimeOffset.Now}",
                Message = $"文件载入出错：\n{ex.Message}",
                Severity = InfoBarSeverity.Error,
                Duration = TimeSpan.FromSeconds(10)
            };

            _mainNotificationService.ShowNotification(notification);
        }
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        IsBackEnabled = NavigationService.CanGoBack;

        if (e.SourcePageType == typeof(SettingsPage))
        {
            Selected = NavigationViewService.SettingsItem;
            RichEditBoxService.SaveToMemory();
            return;
        }

        var selectedItem = NavigationViewService.GetSelectedItem(e.SourcePageType);
        if (selectedItem != null)
        {
            Selected = selectedItem;
        }
    }

    private Task NewFile()
    {
        if (RichEditBoxService.EditorContentEmpty())
        {
            return Task.CompletedTask;
        }

        var notification = new Notification
        {
            Title = $"提示 {DateTimeOffset.Now}",
            Message = $"是否放弃当前文档内容并新建？",
            Severity = InfoBarSeverity.Warning,
            Duration = TimeSpan.FromSeconds(3),
            ActionButton = new Button()
            {
                Content = "放弃",
                Command = new RelayCommand(() =>
                {
                    RichEditBoxService.ClearNew();
                })
            }
        };

        _mainNotificationService.ShowNotification(notification);

        return Task.CompletedTask;
    }

    private void LoadSystemFonts()
    {
        var fontList = Microsoft.Graphics.Canvas.Text.CanvasTextFormat.GetSystemFontFamilies()
            .OrderBy(f => f)
            .Select(f => new Microsoft.UI.Xaml.Media.FontFamily(f));

        foreach (var font in fontList)
        {
            SystemFonts.Add(font);
        }
    }
}
