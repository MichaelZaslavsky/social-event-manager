using System.Collections.Generic;
using System.Linq;

namespace SocialEventManager.Shared.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> enumerable) =>
            !enumerable.Any();

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable) =>
            enumerable?.Any() != true;

        public static bool IsNotNullAndAny<T>(this IEnumerable<T> enumerable) =>
            enumerable?.Any() == true;
    }
}
