using Calculator.Behaviors;
using Calculator.Helpers;
using Calculator.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;

namespace Calculator.Views;

public sealed partial class MainPage : Page
{
    private readonly CancellationTokenSource _navigationCancellationTokenSource = new();

    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
        Unloaded += MainPage_Unloaded;
    }

    private void MainPage_Unloaded(object sender, RoutedEventArgs e)
    {
        _navigationCancellationTokenSource.Cancel();
    }

    private async void MainPageWebView_NavigationCompleted(WebView2 sender, CoreWebView2NavigationCompletedEventArgs args)
    {
        // Check if the navigation was canceled
        if (_navigationCancellationTokenSource.IsCancellationRequested)
        {
            return;
        }

        if (ViewModel != null)
        {
            MainPageLoadingRing.IsActive = false;

            if (args.HttpStatusCode == 200)
            {
                MainPageWebView.Visibility = Visibility.Visible;
                MainPageLoadingFailed.Visibility = Visibility.Collapsed;
            }
            else
            {
                var dialog = new ContentDialog
                {
                    XamlRoot = this.XamlRoot,
                    Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style,
                    Title = "MainPage_WebView_Error_Title".GetLocalized(),
                    Content = "MainPage_WebView_Error_Message".GetLocalized(),
                    CloseButtonText = "MainPage_WebView_Error_Button".GetLocalized()
                };
                _ = await dialog.ShowAsync();

                MainPageWebView.Visibility = Visibility.Collapsed;
                MainPageLoadingFailed.Visibility = Visibility.Visible;
            }
        }
    }
}
