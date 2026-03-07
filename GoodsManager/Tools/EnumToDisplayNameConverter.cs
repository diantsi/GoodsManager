using GoodsManager.Common;
using System.Globalization;

namespace GoodsManager.Tools
{
    public class EnumToDisplayNameConverter : IValueConverter
    {

        // If the value is null, return an empty string to avoid crashes
        // If it's not an Enum, just return the string representation
        // Use our extension method to fetch the Display attribute name
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is null)
                return string.Empty;

            if (value is not Enum castedEnum)
                return value.ToString() ?? string.Empty;

            return castedEnum.GetDisplayName();
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}