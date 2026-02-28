using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace AnotherToolBox.Converters;

public class IndexToDividerVisibilityConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int index)
        {
            return index == 3; // Show divider after 4th item (index 3)
        }
        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}