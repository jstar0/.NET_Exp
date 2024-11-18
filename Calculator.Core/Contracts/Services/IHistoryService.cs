using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Core.Models;

namespace Calculator.Core.Contracts.Services;
public interface IHistoryService
{
    ObservableCollection<HistoryModel> History
    {
        get;
    }

    public void AddHistory(string expression, string result);
}
