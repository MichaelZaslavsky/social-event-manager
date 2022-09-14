using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers;
using Xunit;

namespace SocialEventManager.Tests.DataMembers.Common;

internal static class StringData
{
    public static TheoryData<string?, bool> NullOrEmptyData =>
        new()
        {
            { null, true },
            { string.Empty, true },
            { RandomGeneratorHelpers.GenerateRandomValue(), false },
        };

    public static TheoryData<string?> NullOrWhiteSpaceData =>
        new()
        {
            { null },
            { string.Empty },
            { " " },
        };

    public static TheoryData<string, bool> WhiteSpaceData =>
        new() { { " ", true } };

    public static TheoryData<string, string?, StringComparison, string> TakeAfterData =>
        new()
        {
            { DataConstants.RandomText, "Random ", StringComparison.Ordinal, "Text" },
            { DataConstants.RandomText, "random ", StringComparison.Ordinal, DataConstants.RandomText },
            { DataConstants.RandomText, "random ", StringComparison.OrdinalIgnoreCase, "Text" },
            { DataConstants.RandomText, "Random", StringComparison.Ordinal, " Text" },
            { DataConstants.RandomText, "Random Text", StringComparison.Ordinal, string.Empty },
            { DataConstants.RandomText, null, StringComparison.Ordinal, DataConstants.RandomText },
            { DataConstants.RandomText, string.Empty, StringComparison.Ordinal, DataConstants.RandomText },
            { DataConstants.RandomText, RandomGeneratorHelpers.GenerateRandomValue(), StringComparison.Ordinal, DataConstants.RandomText },
        };

    public static TheoryData<string, string, StringComparison, string> TakeAfterFirstData =>
        new()
        {
            { $"{DataConstants.RandomText} {DataConstants.RandomText}", "Random", StringComparison.Ordinal, $" Text {DataConstants.RandomText}" },
        };

    public static TheoryData<string, string, StringComparison, string> TakeAfterLastData =>
        new()
        {
            { $"{DataConstants.RandomText} {DataConstants.RandomText}", "Random", StringComparison.Ordinal, " Text" },
        };

    public static TheoryData<string, string?, StringComparison, string> TakeUntilData =>
        new()
        {
            { DataConstants.RandomText, " Text", StringComparison.Ordinal, "Random" },
            { DataConstants.RandomText, " ", StringComparison.Ordinal, "Random" },
            { DataConstants.RandomText, "Text", StringComparison.Ordinal, "Random " },
            { DataConstants.RandomText, "text", StringComparison.Ordinal, DataConstants.RandomText },
            { DataConstants.RandomText, "text", StringComparison.OrdinalIgnoreCase, "Random " },
            { DataConstants.RandomText, "Random Text", StringComparison.Ordinal, string.Empty },
            { DataConstants.RandomText, null, StringComparison.Ordinal, DataConstants.RandomText },
            { DataConstants.RandomText, string.Empty, StringComparison.Ordinal, DataConstants.RandomText },
            { DataConstants.RandomText, RandomGeneratorHelpers.GenerateRandomValue(), StringComparison.Ordinal, DataConstants.RandomText },
        };

    public static TheoryData<string, string, StringComparison, string> TakeUntilFirstData =>
        new()
        {
            { $"{DataConstants.RandomText} {DataConstants.RandomText}", "Text", StringComparison.Ordinal, "Random " },
        };

    public static TheoryData<string, string, StringComparison, string> TakeUntilLastData =>
        new()
        {
            { $"{DataConstants.RandomText} {DataConstants.RandomText}", "Text", StringComparison.Ordinal, "Random Text Random " },
        };
}
