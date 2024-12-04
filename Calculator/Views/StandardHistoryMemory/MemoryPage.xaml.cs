using System.Collections.Specialized;
using Calculator.Core.Models;
using Calculator.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Windows.ApplicationModel.DataTransfer;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Calculator.Views.StandardHistoryMemory;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MemoryPage : Page
{
    public CalculateStandardViewModel ViewModel
    {
        get;
    }

    public MemoryPage()
    {
        ViewModel = App.GetService<CalculateStandardViewModel>();
        InitializeComponent();
        DataContext = ViewModel;
        ViewModel.Memory.CollectionChanged += UpdateMemoryEmptyVisibility;
        UpdateMemoryEmptyVisibility(null, null);
    }

    private void UpdateMemoryEmptyVisibility(object? sender, NotifyCollectionChangedEventArgs? e)
    {
        MemoryEmptyNotice.Visibility = ViewModel.Memory.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        ClearMemoryButton.Visibility = ViewModel.Memory.Count == 0 ? Visibility.Collapsed : Visibility.Visible;
    }

    private void ClearMemory_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.Memory.Clear();
    }

    private void MemoryCopy_Click(object sender, RoutedEventArgs e)
    {
        // Copy MemoryModel.Result into clipboard
        if (sender is FrameworkElement { DataContext: MemoryModel memoryModel })
        {
            var dataPackage = new DataPackage();
            dataPackage.SetText(memoryModel.Result);
            Clipboard.SetContent(dataPackage);
        }
    }

    private void MemoryDelete_Click(object sender, RoutedEventArgs e)
    {
        // Delete selected MemoryModel
        if (sender is FrameworkElement { DataContext: MemoryModel memoryModel })
        {
            ViewModel.RemoveMemory(memoryModel);
        }
    }

    private void MemoryListView_ItemClick(object sender, ItemClickEventArgs e)
    {
        // do nothing
    }

    private void MemoryItemMc_OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is FrameworkElement { DataContext: MemoryModel memoryModel })
        {
            ViewModel.OnButtonTargetMcClicked(memoryModel);
        }
    }

    private void MemoryItemMp_OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is FrameworkElement { DataContext: MemoryModel memoryModel })
        {
            ViewModel.OnButtonTargetMpClicked(memoryModel);
        }
    }

    private void MemoryItemMm_OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is FrameworkElement { DataContext: MemoryModel memoryModel })
        {
            ViewModel.OnButtonTargetMmClicked(memoryModel);
        }
    }

    private void MemoryItemStackPanel_OnPointerEntered(object sender, PointerRoutedEventArgs e)
    {
        var item = sender as FrameworkElement;
        if (item != null)
        {
            var memoryItemMc = item.FindName("MemoryItemMc") as Button;
            if (memoryItemMc != null)
            {
                memoryItemMc.Opacity = 1;
            }
            var memoryItemMp = item.FindName("MemoryItemMp") as Button;
            if (memoryItemMp != null)
            {
                memoryItemMp.Opacity = 1;
            }
            var memoryItemMm = item.FindName("MemoryItemMm") as Button;
            if (memoryItemMm != null)
            {
                memoryItemMm.Opacity = 1;
            }
        }
    }

    private void MemoryItemStackPanel_OnPointerExited(object sender, PointerRoutedEventArgs e)
    {
        var item = sender as FrameworkElement;
        if (item != null)
        {
            var memoryItemMc = item.FindName("MemoryItemMc") as Button;
            if (memoryItemMc != null)
            {
                memoryItemMc.Opacity = 0;
            }
            var memoryItemMp = item.FindName("MemoryItemMp") as Button;
            if (memoryItemMp != null)
            {
                memoryItemMp.Opacity = 0;
            }
            var memoryItemMm = item.FindName("MemoryItemMm") as Button;
            if (memoryItemMm != null)
            {
                memoryItemMm.Opacity = 0;
            }
        }
    }
}
