using System.Collections.ObjectModel;
using Calculator.Core.Models;

namespace Calculator.Core.Contracts.Services;
public interface IHistoryService
{
    ObservableCollection<HistoryModel> History
    {
        get;
    }

    void AddHistory(string expression, string result);

    void RemoveHistory(HistoryModel historyModel);
}
