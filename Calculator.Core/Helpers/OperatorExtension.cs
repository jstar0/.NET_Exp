using Calculator.Core.Models;

namespace Calculator.Core.Helpers;
public static class OperatorExtension
{
    public static string GetOperatorSymbol(this OperatorType operatorType)
    {
        return operatorType switch
        {
            OperatorType.Null => "null",
            OperatorType.Add => "+",
            OperatorType.Subtract => "-",
            OperatorType.Multiply => "×",
            OperatorType.Divide => "÷",
            OperatorType.Equal => "=",
            _ => ""
        };
    }
}
