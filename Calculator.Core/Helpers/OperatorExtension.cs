using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Core.Models;

namespace Calculator.Core.Helpers;
public static class OperatorExtension
{
    public static string GetOperatorSymbol(this OperatorType operatorType)
    {
        return operatorType switch
        {
            OperatorType.Add => "+",
            OperatorType.Subtract => "-",
            OperatorType.Multiply => "*",
            OperatorType.Divide => "/",
            OperatorType.Invert => "1/x",
            OperatorType.XPower2 => "x^2",
            OperatorType.Sqrt => "sqrt",
            OperatorType.Percent => "%",
            OperatorType.Equal => "=",
            _ => ""
        };
    }
}
