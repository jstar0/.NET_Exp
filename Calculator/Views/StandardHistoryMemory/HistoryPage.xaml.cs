using System.Collections.Specialized;
using Calculator.Core.Models;
using Calculator.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.DataTransfer;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Calculator.Views.StandardHistoryMemory;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class HistoryPage : Page
{
    public CalculateStandardViewModel ViewModel
    {
        get;
    }

    public HistoryPage()
    {
        ViewModel = App.GetService<CalculateStandardViewModel>();
        InitializeComponent();
        DataContext = ViewModel;

        ViewModel.History.CollectionChanged += HistoryItems_CollectionChanged;
        UpdateHistoryEmptyVisibility();
    }

    private void HistoryItems_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        UpdateHistoryEmptyVisibility();
    }

    private void UpdateHistoryEmptyVisibility()
    {
        HistoryEmptyNotice.Visibility = ViewModel.History.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        ClearHistoryButton.Visibility = ViewModel.History.Count == 0 ? Visibility.Collapsed : Visibility.Visible;
    }

    private void ClearHistory_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.History.Clear();
    }

    private void HistoryCopy_Click(object sender, RoutedEventArgs e)
    {
        // Copy HistoryModel.Result into clipboard
        if (sender is FrameworkElement { DataContext: HistoryModel historyModel })
        {
            var dataPackage = new DataPackage();
            dataPackage.SetText(historyModel.Result);
            Clipboard.SetContent(dataPackage);
        }
    }

    private void HistoryDelete_Click(object sender, RoutedEventArgs e)
    {
        // Delete selected HistoryModel
        if (sender is FrameworkElement { DataContext: HistoryModel historyModel })
        {
            ViewModel.RemoveHistory(historyModel);
        }
    }

    private void HistoryListView_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (e.ClickedItem is HistoryModel historyModel)
        {
            ViewModel.ClearWithResult(historyModel.Result);
        }
    }
}