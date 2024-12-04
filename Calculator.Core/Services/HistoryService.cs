using System.Collections.ObjectModel;
using Calculator.Core.Contracts.Services;
using Calculator.Core.Models;

namespace Calculator.Core.Services;

public class HistoryService : IHistoryService
{
    private readonly ObservableCollection<HistoryModel> _history = [];

    public ObservableCollection<HistoryModel> History => _history;

    public void AddHistory(string expression, string result)
    {
        _history.Insert(0, new HistoryModel { Expression = expression, Result = result });
    }

    public void RemoveHistory(HistoryModel historyModel)
    {
        _history.Remove(historyModel);
    }
}