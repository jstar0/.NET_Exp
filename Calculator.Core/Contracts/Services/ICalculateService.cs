using System.ComponentModel;
using Calculator.Core.Models;

namespace Calculator.Core.Contracts.Services;
public interface ICalculateService : INotifyPropertyChanged
{
    CalculateModel Calculate
    {
        get;
    }

    void NumpadPress(string number);

    void DecimalPointPress();

    void BackspacePress();

    void CE();

    void C();

    void Reverse();

    void FourOperation(int op);

    void PerformInvert();

    void PerformXPower2();

    void PerformSquareRoot();

    void PerformPercent();

    void PerformEqual();

    void MakeExpression();
}
