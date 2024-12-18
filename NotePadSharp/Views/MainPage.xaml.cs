using Microsoft.UI.Xaml.Controls;

using NotePadSharp.ViewModels;

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
    }
}
