using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Core.Helpers;
public static class CalculateBetweenString
{
    public static string GetStringResult(string operand1, string operand2, string operatorSymbol)
    {
        double result = 0;
        var operand1Double = Convert.ToDouble(operand1);
        var operand2Double = Convert.ToDouble(operand2);
        result = operatorSymbol switch
        {
            "+" => operand1Double + operand2Double,
            "-" => operand1Double - operand2Double,
            "*" => operand1Double * operand2Double,
            "/" => operand1Double / operand2Double,
            _ => result
        };
        return result.ToString(CultureInfo.InvariantCulture);
    }
}
