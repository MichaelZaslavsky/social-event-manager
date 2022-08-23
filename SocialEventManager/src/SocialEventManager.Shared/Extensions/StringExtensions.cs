using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace SocialEventManager.Shared.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? value) =>
        string.IsNullOrEmpty(value);

    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? value) =>
        string.IsNullOrWhiteSpace(value);

    public static string TakeAfterFirst(this string source, string? fromStart, StringComparison comparisonType = StringComparison.Ordinal)
    {
        if (fromStart.IsNullOrEmpty())
        {
            return source;
        }

        int firstIndex = source.IndexOf(fromStart, comparisonType);
        return GetSubstringForStartIndex(source, firstIndex, fromStart.Length);
    }

    public static string TakeAfterLast(this string source, string? fromStart, StringComparison comparisonType = StringComparison.Ordinal)
    {
        if (fromStart.IsNullOrEmpty())
        {
            return source;
        }

        int lastIndex = source.LastIndexOf(fromStart, comparisonType);
        return GetSubstringForStartIndex(source, lastIndex, fromStart.Length);
    }

    public static string TakeUntilFirst(this string source, string? fromEnd, StringComparison comparisonType = StringComparison.Ordinal)
    {
        if (fromEnd.IsNullOrEmpty())
        {
            return source;
        }

        int endIndex = source.IndexOf(fromEnd, comparisonType);
        return GetSubStringForEndIndex(source, endIndex);
    }

    public static string TakeUntilLast(this string source, string? fromEnd, StringComparison comparisonType = StringComparison.Ordinal)
    {
        if (fromEnd.IsNullOrEmpty())
        {
            return source;
        }

        int endStringLastIndex = source.LastIndexOf(fromEnd, comparisonType);
        return GetSubStringForEndIndex(source, endStringLastIndex);
    }

    public static string Encode(this string input)
    {
        byte[] encodedInput = Encoding.UTF8.GetBytes(input);
        return WebEncoders.Base64UrlEncode(encodedInput);
    }

    public static string Decode(this string input)
    {
        byte[] decodedInput = WebEncoders.Base64UrlDecode(input);
        return Encoding.UTF8.GetString(decodedInput);
    }

    #region Private methods

    private static string GetSubstringForStartIndex(string source, int startIndex, int valueLength)
    {
        if (startIndex < 0)
        {
            return source;
        }

        int index = startIndex + valueLength;
        return index >= source.Length
            ? string.Empty
            : source[index..];
    }

    private static string GetSubStringForEndIndex(string source, int endIndex)
    {
        if (endIndex < 0)
        {
            return source;
        }

        if (endIndex == 0)
        {
            return string.Empty;
        }

        return source[..endIndex];
    }

    #endregion Private methods
}
