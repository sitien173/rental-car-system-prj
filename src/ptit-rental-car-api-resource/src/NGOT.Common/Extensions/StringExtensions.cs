namespace NGOT.Common.Extensions;

public static class StringExtensions
{
    public static string UpperFirstLetter(this string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return string.Empty;
        }

        return char.ToUpper(str[0]) + str[1..];
    }
}