using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Core.Contracts.Services;
using Calculator.Core.Helpers;
using Calculator.Core.Models;

namespace Calculator.Core.Services;
public class MemoryService : IMemoryService
{
    private readonly ObservableCollection<MemoryModel> _memory = [];

    public ObservableCollection<MemoryModel> Memory => _memory;

    public void MemoryButtonMc()
    {
        // Clear all items from memory
        _memory.Clear();
    }

    public MemoryModel MemoryButtonMr()
    {
        // Get the last item from memory
        return _memory.FirstOrDefault();
    }

    public void MemoryButtonMp(string number)
    {
        if (_memory.Any())
        {
            var lastItem = _memory.Last();
            lastItem.Result = CalculateBetweenString.GetStringResult(lastItem.Result, number, "+");
            // modify the last item in memory
            _memory[^1] = lastItem;
        }
    }

    public void MemoryButtonMm(string number)
    {
        if (_memory.Any())
        {
            var lastItem = _memory.Last();
            lastItem.Result = CalculateBetweenString.GetStringResult(lastItem.Result, number, "-");
            // modify the last item in memory
            _memory[^1] = lastItem;
        }
    }

    public void MemoryButtonMs(string number)
    {
        // Add number into the last item in memory
        _memory.Add(new MemoryModel { Result = number });
    }

    public void MemoryTargetMc(MemoryModel target)
    {
        // Remove the target item from memory
        _memory.Remove(target);
    }

    public void MemoryTargetMp(MemoryModel target, string number)
    {
        if (_memory.Contains(target))
        {
            target.Result = CalculateBetweenString.GetStringResult(target.Result, number, "+");
            // Update the target item in memory
            var index = _memory.IndexOf(target);
            _memory[index] = target;
        }
    }

    public void MemoryTargetMm(MemoryModel target, string number)
    {
        if (_memory.Contains(target))
        {
            target.Result = CalculateBetweenString.GetStringResult(target.Result, number, "-");
            // Update the target item in memory
            var index = _memory.IndexOf(target);
            _memory[index] = target;
        }
    }
}
