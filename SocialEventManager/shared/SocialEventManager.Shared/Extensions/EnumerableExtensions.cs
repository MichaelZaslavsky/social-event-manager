using System.Data;
using System.Reflection;

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

        public static DataTable ToDataTable<T>(this IEnumerable<T> enumerable)
        {
            DataTable dataTable = new();

            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo p in properties)
            {
                dataTable.Columns.Add(p.Name, p.PropertyType);
            }

            foreach (T item in enumerable)
            {
                object[] values = properties.Select(p => p.GetValue(item)).ToArray();
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable.ToList())
            {
                action(item);
                yield return item;
            }
        }
    }
}
