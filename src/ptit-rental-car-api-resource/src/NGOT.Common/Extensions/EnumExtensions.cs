using System.ComponentModel;

namespace NGOT.Common.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());

        if (fieldInfo?.GetCustomAttributes(
                typeof(DescriptionAttribute), false) is DescriptionAttribute[] { Length: > 0 } attributes)
            return attributes[0].Description;

        return value.ToString();
    }
}