using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Calculator.Core.Models;
public partial class CalculateModel : ObservableObject
{
    public string Operand1
    {
        get;
        set;
    }

    public string Operand2
    {
        get;
        set;
    }

    public bool WillOverwriteInputs
    {
        get;
        set;
    }

    [ObservableProperty]
    private string _result;

    [ObservableProperty]
    private string _expression;

    [ObservableProperty]
    private OperatorType _operator;
}

public enum OperatorType
{
    Add,
    Subtract,
    Multiply,
    Divide,
    Invert,
    XPower2,
    Sqrt,
    Percent,
    Equal
}