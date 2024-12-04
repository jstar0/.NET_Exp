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
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Specialized;

namespace Calculator.ViewModels;

public partial class CalculateStandardViewModel : ObservableRecipient
{
    private readonly IHistoryService _historyService;

    private readonly IMemoryService _memoryService;

    private readonly ICalculateService _calculateService;

    public ObservableCollection<HistoryModel> History => _historyService.History;

    public ObservableCollection<MemoryModel> Memory => _memoryService.Memory;

    #region Old Memory Reverse Method
    
    /*private ObservableCollection<MemoryModel>? _reversedMemory;
    public ObservableCollection<MemoryModel> ReversedMemory
    {
        get
        {
            if (_reversedMemory == null)
            {
                _reversedMemory = new ObservableCollection<MemoryModel>(Memory.Reverse());
                // Subscribe to changes in Memory
                Memory.CollectionChanged += Memory_CollectionChanged;
            }
            return _reversedMemory;
        }
    }

    // Update ReversedMemory when Memory changes
    private void Memory_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        // Clear and repopulate the reversed collection
        //_reversedMemory.CollectionChanged -= ReversedMemory_CollectionChanged;
        _reversedMemory.Clear();
        foreach (var item in Memory.Reverse())
        {
            _reversedMemory.Add(item);
        }
        //_reversedMemory.CollectionChanged += ReversedMemory_CollectionChanged;
    }*/

    #endregion


    [ObservableProperty]
    private CalculateModel _calculateItem;

    //public CalculateModel CalculateItem => _calculateService.Calculate;

    public CalculateStandardViewModel(IHistoryService historyService, ICalculateService calculateService, IMemoryService memoryService)
    {
        _historyService = historyService;
        _calculateService = calculateService;
        _memoryService = memoryService;

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

    #region Calculate

    public ICommand NumpadButtonCommand => new RelayCommand<string>(OnNumberButtonClicked);
    public ICommand DecimalPointButtonCommand => new RelayCommand(_calculateService.DecimalPointPress);
    public ICommand BackspaceButtonCommand => new RelayCommand(_calculateService.BackspacePress);
    public ICommand ReverseButtonCommand => new RelayCommand(_calculateService.Reverse);
    public ICommand CEButtonCommand => new RelayCommand(_calculateService.CE);
    public ICommand CButtonCommand => new RelayCommand(_calculateService.C);
    public ICommand InvertButtonCommand => new RelayCommand(OnInvertButtonClicked);
    public ICommand XPower2ButtonCommand => new RelayCommand(OnXPower2ButtonClicked);
    public ICommand SquareRootButtonCommand => new RelayCommand(OnSquareRootButtonClicked);
    public ICommand PercentButtonCommand => new RelayCommand(_calculateService.PerformPercent);
    public ICommand FourOperationCommand => new RelayCommand<string>(OnFourOperationClicked);
    public ICommand EqualCommand => new RelayCommand(OnEqualButtonClicked);

    /* 使用 Action exec 作为参数
    public ICommand InvertButtonCommand => new RelayCommand(() => OnFunctionButtonClicked(_calculateService.PerformInvert));
    
    // If there's a return type: Func<RETURN TYPE> exec
    private void OnFunctionButtonClicked(Action exec)
    {
        var tmpResult = CalculateItem.Result;
        exec();
        _historyService.AddHistory($"1/({tmpResult}) =", CalculateItem.Result);
    }*/

    private void OnInvertButtonClicked()
    {
        var tmpResult = CalculateItem.Result;
        _calculateService.PerformInvert();
        _historyService.AddHistory($"1/({tmpResult}) =", CalculateItem.Result);
    }

    private void OnXPower2ButtonClicked()
    {
        var tmpResult = CalculateItem.Result;
        _calculateService.PerformXPower2();
        _historyService.AddHistory($"1/({tmpResult}) =", CalculateItem.Result);
    }

    private void OnSquareRootButtonClicked()
    {
        var tmpResult = CalculateItem.Result;
        _calculateService.PerformSquareRoot();
        _historyService.AddHistory($"1/({tmpResult}) =", CalculateItem.Result);
    }

    private void OnEqualButtonClicked()
    {
        _calculateService.PerformEqual();
        _historyService.AddHistory(CalculateItem.Expression, CalculateItem.Result);
    }

    private void OnNumberButtonClicked(string? number)
    {
        if (number == null)
        {
            return;
        }

        _calculateService.NumpadPress(number);
    }

    private void OnFourOperationClicked(string? op)
    {
        _calculateService.FourOperation(int.Parse(op!));
    }

    public void ClearWithResult(string result)
    {
        CalculateItem.Operand1 = null;
        CalculateItem.Operand2 = null;
        CalculateItem.WillClearExpression = false;
        CalculateItem.WillOverwriteInputs = false;
        CalculateItem.Operator = OperatorType.Null;
        _calculateService.MakeExpression();
        CalculateItem.Result = (decimal.TryParse(result, out _)) ? result : "";
    }

    #endregion

    #region Menu

    public ICommand TextBlockCopy => new RelayCommand(TextBlockOnCopy);

    private void TextBlockOnCopy()
    {
        if (!decimal.TryParse(CalculateItem.Result, out var v))
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
        if (package.Contains(StandardDataFormats.Text))
        {
            var text = await package.GetTextAsync();
            if (decimal.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var v))
            {
                CalculateItem.Result = v.ToString(CultureInfo.InvariantCulture);
            }
        }
    }

    #endregion

    #region Memory

    public ICommand ButtonMcCommand => new RelayCommand(OnButtonMcClicked);
    public ICommand ButtonMrCommand => new RelayCommand(OnButtonMrClicked);
    public ICommand ButtonMpCommand => new RelayCommand(OnButtonMpClicked);
    public ICommand ButtonMmCommand => new RelayCommand(OnButtonMmClicked);
    public ICommand ButtonMsCommand => new RelayCommand(OnButtonMsClicked);

    private void OnButtonMcClicked()
    {
        _memoryService.MemoryButtonMc();
    }

    private void OnButtonMrClicked()
    {
        if (_memoryService.Memory.Count > 0 && decimal.TryParse(_memoryService.MemoryButtonMr().Result, out var result))
        {
            CalculateItem.Result = Convert.ToString(result, CultureInfo.InvariantCulture);
        }
    }

    private void OnButtonMpClicked()
    {
        _memoryService.MemoryButtonMp(CalculateItem.Result);
    }

    private void OnButtonMmClicked()
    {
        _memoryService.MemoryButtonMm(CalculateItem.Result);
    }

    private void OnButtonMsClicked()
    {
        _memoryService.MemoryButtonMs(CalculateItem.Result);
    }

    public void OnButtonTargetMcClicked(MemoryModel item)
    {
        _memoryService.MemoryTargetMc(item);
    }

    public void OnButtonTargetMpClicked(MemoryModel item)
    {
        _memoryService.MemoryTargetMp(item, CalculateItem.Result);
    }

    public void OnButtonTargetMmClicked(MemoryModel item)
    {
        _memoryService.MemoryTargetMm(item, CalculateItem.Result);
    }

    #endregion

    #region History
    public void RemoveHistory(HistoryModel historyModel)
    {
        _historyService.RemoveHistory(historyModel);
    }

    public void RemoveMemory(MemoryModel memoryModel)
    {
        _memoryService.RemoveMemory(memoryModel);
    }

    #endregion

}