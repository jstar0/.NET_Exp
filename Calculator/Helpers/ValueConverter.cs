using System.Globalization;
using Microsoft.UI.Xaml.Data;

namespace Calculator.Helpers;
public partial class NumberFormatterConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is string stringValue && decimal.TryParse(stringValue, out var decimalValue))
        {
            // 判断数值范围
            if (Math.Abs(decimalValue) >= 1e20m || Math.Abs(decimalValue) <= 0.0000001m && Math.Abs(decimalValue) != 0)
            {
                // 使用科学计数法
                return decimalValue.ToString("E2", CultureInfo.InvariantCulture);
            }

            // 使用千位分隔符
            var formattedValue = decimalValue.ToString("N", CultureInfo.InvariantCulture);

            // 保留原始小数点后的零
            if (stringValue.Contains('.'))
            {
                var decimalPart = stringValue.Split('.')[1];
                return formattedValue.Split('.')[0] + "." + (decimalPart.Length > 0 ? decimalPart : string.Empty);
            }
            return formattedValue.Split('.')[0];
        }
        return "InvalidString".GetLocalized();
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}

