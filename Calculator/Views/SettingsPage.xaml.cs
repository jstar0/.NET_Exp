using System.Collections.ObjectModel;
using Windows.ApplicationModel.Core;
using Calculator.Contracts.Services;
using Calculator.Helpers;
using Calculator.Services;
using Calculator.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Windows.AppLifecycle;

namespace Calculator.Views;

// TODO: Set the URL for your privacy policy by updating SettingsPage_PrivacyTermsLink.NavigateUri in Resources.resw.
public sealed partial class SettingsPage : Page
{
    public SettingsViewModel ViewModel
    {
        get;
    }

    private readonly List<string> LanguageList =
    [
        "Default",
        "English",
        "中文"
    ];

    private bool isLanguageLoaded = false;

    private readonly ILanguageService _languageService;

    public SettingsPage()
    {
        ViewModel = App.GetService<SettingsViewModel>();
        _languageService = App.GetService<ILanguageService>();
        InitializeComponent();
    }

    private async void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!isLanguageLoaded)
        {
            return;
        }

        var comboBox = sender as ComboBox;
        var selected = comboBox?.SelectedItem as string;

        switch (selected)
        {
            case "Default":
                await _languageService.SetLanguageAsync(ILanguageService.Lang.Default);
                break;
            case "English":
                await _languageService.SetLanguageAsync(ILanguageService.Lang.English);
                break;
            case "中文":
                await _languageService.SetLanguageAsync(ILanguageService.Lang.Chinese);
                break;
        }

        // Show a dialog to inform the user that the app needs to be restarted for the language change to take effect
        // This is a common pattern for apps that support runtime language switching
        var dialog = new ContentDialog
        {
            XamlRoot = this.XamlRoot,
            Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style,
            Title = "Settings_Language_Dialog_Title".GetLocalized(),
            PrimaryButtonText = "Settings_Language_Dialog_Button_Confirm".GetLocalized(),
            CloseButtonText = "Settings_Language_Dialog_Button_Reject".GetLocalized(),
            DefaultButton = ContentDialogButton.Primary,
            Content = new TextBlock { Text = "Settings_Language_Dialog_Content".GetLocalized() },
            RequestedTheme = ActualTheme
        };

        var result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            AppInstance.Restart("");
        }

    }

    private async void LanguageComboBox_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var comboBox = sender as ComboBox;
        if (comboBox != null)
        {
            comboBox.SelectedItem = await _languageService.LoadLanguageFromSettingsAsync() switch
            {
                ILanguageService.Lang.Default => "Default",
                ILanguageService.Lang.English => "English",
                ILanguageService.Lang.Chinese => "中文",
                _ => comboBox.SelectedItem
            };
        }

        isLanguageLoaded = true;
    }
}
