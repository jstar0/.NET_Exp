using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    void PerformCalculate(OperatorType operatorType);

    void PerformFourOperation(OperatorType operatorType);

    void PerformInvert();

    void PerformXPower2();

    void PerformSquareRoot();

    void PerformPercent();

    void PerformEqual();
}
