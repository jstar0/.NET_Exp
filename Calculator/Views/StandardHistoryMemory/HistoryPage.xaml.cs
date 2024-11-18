using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Calculator.Core.Models;
using Calculator.ViewModels;
using Windows.ApplicationModel.DataTransfer;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Calculator.Views
{
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
            this.InitializeComponent();
            DataContext = ViewModel;

            ViewModel.History.CollectionChanged += HistoryItems_CollectionChanged;
            UpdateHistoryEmptyVisibility();
            ViewModel.PerformCalculation("abc+66");
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
    }
}
