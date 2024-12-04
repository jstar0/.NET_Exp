using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Core.Models;

namespace Calculator.Core.Helpers;
public static class CalculateBetweenString
{
    public static string GetStringResult(string operand1, string operand2, OperatorType operatorType)
    {
        try
        {
            double result = 0;
            var operand1Double = Convert.ToDouble(operand1);
            var operand2Double = Convert.ToDouble(operand2);
            result = operatorType switch
            {
                OperatorType.Add => operand1Double + operand2Double,
                OperatorType.Subtract => operand1Double - operand2Double,
                OperatorType.Multiply => operand1Double * operand2Double,
                OperatorType.Divide => operand1Double / operand2Double,
                _ => result
            };
            return result.ToString("0.####################", CultureInfo.InvariantCulture);
        }
        catch
        {
            return "InvalidString";
        }
    }

    // 1 / number
    public static string InvertNumber(string number)
    {
        try
        {
            return GetStringResult("1", number, OperatorType.Divide);
        }
        catch
        {
            return "InvalidString";
        }
    }

    // number ^ 2
    public static string XPower2(string number)
    {
        try
        {
            var result = Math.Pow(Convert.ToDouble(number), 2);
            return result.ToString(CultureInfo.InvariantCulture);
        }
        catch
        {
            return "InvalidString";
        }
    }

    // sqrt (number)
    public static string SquareRoot(string number)
    {
        try
        {
            if (number == "0")
            {
                return "0";
            }
            var result = Math.Sqrt(Convert.ToDouble(number));
            return result.ToString("0.####################", CultureInfo.InvariantCulture);
        }
        catch
        {
            return "InvalidString";
        }
    }

    // percentage of number (number / 100)
    public static string PercentNumber(string number)
    {
        try
        {
            return GetStringResult(number, "100", OperatorType.Divide);
        }
        catch
        {
            return "InvalidString";
        }
    }
}
