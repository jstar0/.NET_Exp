using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;
using Calculator.Contracts.Services;
using Calculator.Core.Contracts.Services;
using Calculator.Core.Models;
using Calculator.Core.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Windows.ApplicationModel.DataTransfer;
using Calculator.Helpers;

namespace Calculator.ViewModels;

public partial class CalculateStandardViewModel : ObservableRecipient
{
    private readonly IHistoryService _historyService;

    private readonly ICalculateService _calculateService;

    public ObservableCollection<HistoryModel> History => _historyService.History;

    [ObservableProperty]
    private CalculateModel _calculateItem;

    //public CalculateModel CalculateItem => _calculateService.Calculate;

    public CalculateStandardViewModel(IHistoryService historyService, ICalculateService calculateService)
    {
        _historyService = historyService;
        _calculateService = calculateService;

        // Initialize CalculateItem
        CalculateItem = _calculateService.Calculate;

        // Subscribe to PropertyChanged event
        _calculateService.PropertyChanged += CalculateService_PropertyChanged;
    }

    private void CalculateService_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_calculateService.Calculate))
        {
            CalculateItem = _calculateService.Calculate;
        }
    }

    public ICommand TextBlockCopy => new RelayCommand(TextBlockOnCopy);

    private void TextBlockOnCopy()
    {
        if (!double.TryParse(CalculateItem.Result, out var v))
        {
            return;
        }

        var package = new DataPackage();
        package.SetText(v.ToString(CultureInfo.InvariantCulture));
        Clipboard.SetContent(package);
    }

    public ICommand TextBlockPaste => new RelayCommand(TextBlockOnPaste);

    private async void TextBlockOnPaste()
    {
        var package = Clipboard.GetContent();
        if (package.Contains(StandardDataFormats.Text) && double.TryParse(await package.GetTextAsync(), out var v))
        {
            CalculateItem.Result = v.ToString(CultureInfo.InvariantCulture);
        }
    }

    public ICommand NumpadButtonCommand => new RelayCommand<string>(OnNumberButtonClicked);

    private void OnNumberButtonClicked(string? number)
    {
        if (number == null)
        {
            return;
        }

        _calculateService.NumpadPress(number);
    }

    public ICommand DecimalPointButtonCommand => new RelayCommand(_calculateService.DecimalPointPress);
    public ICommand BackspaceButtonCommand => new RelayCommand(_calculateService.BackspacePress);

    public ICommand ButtonMcCommand => new RelayCommand(OnButtonMcClicked);
    public ICommand ButtonMrCommand => new RelayCommand(OnButtonMcClicked);
    public ICommand ButtonMpCommand => new RelayCommand(OnButtonMcClicked);
    public ICommand ButtonMmCommand => new RelayCommand(OnButtonMcClicked);
    public ICommand ButtonMsCommand => new RelayCommand(OnButtonMcClicked);

    private void OnButtonMcClicked()
    {
        //throw new NotImplementedException();
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