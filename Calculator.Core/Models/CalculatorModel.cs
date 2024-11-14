using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Core.Models;
public class CalculatorModel
{
    public double Operand1
    {
        get;
        set;
    }
    public double Operand2
    {
        get;
        set;
    }
    public double Result
    {
        get;
        set;
    }
    public string Operator
    {
        get;
        set;
    }

    public CalculatorModel()
    {
        Operand1 = 0;
        Operand2 = 0;
        Result = 0;
        Operator = string.Empty;
    }
}
