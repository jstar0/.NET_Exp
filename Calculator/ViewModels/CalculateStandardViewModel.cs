using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Calculator.Contracts.Services;
using Calculator.Core.Contracts.Services;
using Calculator.Core.Models;
using Calculator.Core.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Windows.ApplicationModel.DataTransfer;

namespace Calculator.ViewModels;

public partial class CalculateStandardViewModel : ObservableRecipient
{
    private readonly IHistoryService _historyService;

    public ObservableCollection<HistoryModel> History => _historyService.History;

    public CalculateStandardViewModel(IHistoryService historyService)
    {
        _historyService = historyService;
    }

    // Method to perform calculation and add to history
    public void PerformCalculation(string expression)
    {
        // Perform calculation logic...
        string result = "666"/* calculation result */;

        // Add to history
        _historyService.AddHistory(expression, result);
    }

    public void RemoveHistory(HistoryModel historyModel)
    {
        _historyService.RemoveHistory(historyModel);
    }
}