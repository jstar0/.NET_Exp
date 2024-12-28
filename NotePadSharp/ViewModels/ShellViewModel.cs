using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.WinUI.Behaviors;
using Microsoft.UI.Xaml.Navigation;

using NotePadSharp.Contracts.Services;
using NotePadSharp.Views;
using Microsoft.UI.Xaml.Controls;

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

    public ICommand OnFileOpen => new AsyncRelayCommand(FileOpen);
    public ICommand OnFileSave => new AsyncRelayCommand(FileSave);

    public ShellViewModel(INavigationService navigationService, INavigationViewService navigationViewService, IRichEditBoxService richEditBoxService, IMainNotificationService mainNotificationService)
    {
        NavigationService = navigationService;
        NavigationService.Navigated += OnNavigated;
        NavigationViewService = navigationViewService;
        RichEditBoxService = richEditBoxService;
        _mainNotificationService = mainNotificationService;
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
        catch(Exception ex)
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
}
