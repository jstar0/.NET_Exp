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

    public bool WillClearExpression
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
    Null,
    Add,
    Subtract,
    Multiply,
    Divide,
    Equal
}