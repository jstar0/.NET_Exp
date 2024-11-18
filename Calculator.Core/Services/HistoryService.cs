using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Calculator.Core.Contracts.Services;
using Calculator.Core.Models;

namespace Calculator.Core.Services;

public class HistoryService : IHistoryService
{
    private readonly ObservableCollection<HistoryModel> _history = [];

    public ObservableCollection<HistoryModel> History => _history;

    public void AddHistory(string expression, string result)
    {
        _history.Add(new HistoryModel { Expression = expression, Result = result });
    }
}