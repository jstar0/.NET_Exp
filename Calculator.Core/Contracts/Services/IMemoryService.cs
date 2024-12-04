using System.Collections.ObjectModel;
using Calculator.Core.Models;

namespace Calculator.Core.Contracts.Services;
public interface IMemoryService
{
    ObservableCollection<MemoryModel> Memory
    {
        get;
    }

    void MemoryButtonMc();

    MemoryModel MemoryButtonMr();

    void MemoryButtonMp(string number);

    void MemoryButtonMm(string number);

    void MemoryButtonMs(string number);

    void MemoryTargetMc(MemoryModel target);

    void MemoryTargetMp(MemoryModel target, string number);

    void MemoryTargetMm(MemoryModel target, string number);

    void RemoveMemory(MemoryModel memoryModel);
}