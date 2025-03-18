using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;

namespace ControlCatalog.Converter;

public class HexConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var str = value?.ToString();
        if (str == null)
            return AvaloniaProperty.UnsetValue;
        if (int32.TryParse(str, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int32 x))
            return (decimal)x;
        return AvaloniaProperty.UnsetValue;

    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        try
        {
            if (value is decimal d)
                return ((int32)d).ToString("X8");
            return AvaloniaProperty.UnsetValue;
        }
        catch
        {
            return AvaloniaProperty.UnsetValue;
        }
    }
}
