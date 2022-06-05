using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers;

namespace SocialEventManager.Tests.DataMembers.Common
{
    public static class StringData
    {
        public static IEnumerable<object[]> NullOrEmptyData
        {
            get
            {
                yield return new object[] { null, true };
                yield return new object[] { string.Empty, true };
                yield return new object[] { RandomGeneratorHelpers.GenerateRandomValue(), false };
            }
        }

        public static IEnumerable<object[]> WhiteSpaceData
        {
            get
            {
                yield return new object[] { " ", true };
            }
        }

        public static IEnumerable<object[]> TakeAfterData
        {
            get
            {
                yield return new object[] { DataConstants.RandomText, "Random ", StringComparison.Ordinal, "Text" };
                yield return new object[] { DataConstants.RandomText, "random ", StringComparison.Ordinal, DataConstants.RandomText };
                yield return new object[] { DataConstants.RandomText, "random ", StringComparison.OrdinalIgnoreCase, "Text" };
                yield return new object[] { DataConstants.RandomText, "Random", StringComparison.Ordinal, " Text" };
                yield return new object[] { DataConstants.RandomText, "Random Text", StringComparison.Ordinal, string.Empty };
                yield return new object[] { DataConstants.RandomText, null, StringComparison.Ordinal, DataConstants.RandomText };
                yield return new object[] { DataConstants.RandomText, string.Empty, StringComparison.Ordinal, DataConstants.RandomText };
                yield return new object[] { DataConstants.RandomText, RandomGeneratorHelpers.GenerateRandomValue(), StringComparison.Ordinal, DataConstants.RandomText };
            }
        }

        public static IEnumerable<object[]> TakeAfterFirstData
        {
            get
            {
                yield return new object[] { $"{DataConstants.RandomText} {DataConstants.RandomText}", "Random", StringComparison.Ordinal, $" Text {DataConstants.RandomText}" };
            }
        }

        public static IEnumerable<object[]> TakeAfterLastData
        {
            get
            {
                yield return new object[] { $"{DataConstants.RandomText} {DataConstants.RandomText}", "Random", StringComparison.Ordinal, " Text" };
            }
        }

        public static IEnumerable<object[]> TakeUntilData
        {
            get
            {
                yield return new object[] { DataConstants.RandomText, " Text", StringComparison.Ordinal, "Random" };
                yield return new object[] { DataConstants.RandomText, " ", StringComparison.Ordinal, "Random" };
                yield return new object[] { DataConstants.RandomText, "Text", StringComparison.Ordinal, "Random " };
                yield return new object[] { DataConstants.RandomText, "text", StringComparison.Ordinal, DataConstants.RandomText };
                yield return new object[] { DataConstants.RandomText, "text", StringComparison.OrdinalIgnoreCase, "Random " };
                yield return new object[] { DataConstants.RandomText, "Random Text", StringComparison.Ordinal, string.Empty };
                yield return new object[] { DataConstants.RandomText, null, StringComparison.Ordinal, DataConstants.RandomText };
                yield return new object[] { DataConstants.RandomText, string.Empty, StringComparison.Ordinal, DataConstants.RandomText };
                yield return new object[] { DataConstants.RandomText, RandomGeneratorHelpers.GenerateRandomValue(), StringComparison.Ordinal, DataConstants.RandomText };
            }
        }

        public static IEnumerable<object[]> TakeUntilFirstData
        {
            get
            {
                yield return new object[] { $"{DataConstants.RandomText} {DataConstants.RandomText}", "Text", StringComparison.Ordinal, "Random " };
            }
        }

        public static IEnumerable<object[]> TakeUntilLastData
        {
            get
            {
                yield return new object[] { $"{DataConstants.RandomText} {DataConstants.RandomText}", "Text", StringComparison.Ordinal, "Random Text Random " };
            }
        }
    }
}
