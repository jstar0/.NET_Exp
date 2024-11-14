using System.Globalization;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Calculator.ViewModels;

using Microsoft.UI.Xaml.Controls;
using Calculator.Contracts.Services;
using Microsoft.UI.Xaml;
using Calculator.Helpers;
using Calculator.Services;
using Calculator.Behaviors;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Hosting;
using Windows.ApplicationModel.DataTransfer;

namespace Calculator.Views;

public sealed partial class CalculateStandardPage : Page
{
    public CalculateStandardViewModel ViewModel
    {
        get;
    }

    private int _previousSelectedIndex = 0;

    public CalculateStandardPage()
    {
        ViewModel = App.GetService<CalculateStandardViewModel>();
        InitializeComponent();
        DataContext = ViewModel;

        // 本页禁用标题
        NavigationViewHeaderBehavior.SetHeaderMode(this, NavigationViewHeaderMode.Never);
    }

    private void ToggleRightContentButton_Click(object sender, RoutedEventArgs e)
    {
        App.MainWindow.Width += 400;
    }

    private void SelectorBar_SelectionChanged(SelectorBar sender, SelectorBarSelectionChangedEventArgs args)
    {
        var selectedItem = sender.SelectedItem;
        var currentSelectedIndex = sender.Items.IndexOf(selectedItem);

        var pageType = currentSelectedIndex switch
        {
            0 => typeof(HistoryPage),
            _ => typeof(MemoryPage)
        };

        var slideNavigationTransitionEffect = currentSelectedIndex - _previousSelectedIndex > 0 ? SlideNavigationTransitionEffect.FromRight : SlideNavigationTransitionEffect.FromLeft;

        HistoryMemoryFrame.Navigate(pageType, null, new SlideNavigationTransitionInfo() { Effect = slideNavigationTransitionEffect });

        _previousSelectedIndex = currentSelectedIndex;
    }

    private void NormalOutPutContainerCopyText_Click(object sender, RoutedEventArgs e)
    {
        if (!double.TryParse(NormalOutput.Text, out var v))
        {
            return;
        }

        var package = new DataPackage();
        package.SetText(v.ToString(CultureInfo.InvariantCulture));
        Clipboard.SetContent(package);
    }

    private async void NormalOutPutContainerPasteText_Click(object sender, RoutedEventArgs e)
    {
        var package = Clipboard.GetContent();
        if (package.Contains(StandardDataFormats.Text) && double.TryParse(await package.GetTextAsync(), out var v))
        {
            NormalOutput.Text = v.ToString(CultureInfo.InvariantCulture);
        }
    }
}
