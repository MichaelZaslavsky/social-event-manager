using System.Diagnostics.CodeAnalysis;

namespace SocialEventManager.Shared.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? value) =>
        string.IsNullOrEmpty(value);

    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? value) =>
        string.IsNullOrWhiteSpace(value);

    public static string TakeAfterFirst(this string source, string fromStart, StringComparison comparisonType = StringComparison.Ordinal)
    {
        if (fromStart.IsNullOrEmpty())
        {
            return source;
        }

        int firstIndex = source.IndexOf(fromStart, comparisonType);
        return GetSubstringForStartIndex(source, firstIndex, fromStart.Length);
    }

    public static string TakeAfterLast(this string source, string fromStart, StringComparison comparisonType = StringComparison.Ordinal)
    {
        if (fromStart.IsNullOrEmpty())
        {
            return source;
        }

        int lastIndex = source.LastIndexOf(fromStart, comparisonType);
        return GetSubstringForStartIndex(source, lastIndex, fromStart.Length);
    }

    public static string TakeUntilFirst(this string source, string fromEnd, StringComparison comparisonType = StringComparison.Ordinal)
    {
        if (fromEnd.IsNullOrEmpty())
        {
            return source;
        }

        int endIndex = source.IndexOf(fromEnd, comparisonType);
        return GetSubStringForEndIndex(source, endIndex);
    }

    public static string TakeUntilLast(this string source, string fromEnd, StringComparison comparisonType = StringComparison.Ordinal)
    {
        if (fromEnd.IsNullOrEmpty())
        {
            return source;
        }

        int endStringLastIndex = source.LastIndexOf(fromEnd, comparisonType);
        return GetSubStringForEndIndex(source, endStringLastIndex);
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

    #endregion Private Methods
}
