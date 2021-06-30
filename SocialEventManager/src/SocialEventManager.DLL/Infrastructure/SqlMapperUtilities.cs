using System;
using System.Reflection;
using Dapper.Contrib.Extensions;
using SocialEventManager.Shared.Extensions;

namespace SocialEventManager.DAL.Infrastructure
{
    public static class SqlMapperUtilities
    {
        public static string GetTableName<T>(bool includeSchema = true) =>
            GetTableName(typeof(T), includeSchema);

        public static string GetTableName(Type type, bool includeSchema = true)
        {
            if (SqlMapperExtensions.TableNameMapper != null)
            {
                return SqlMapperExtensions.TableNameMapper(type);
            }

            const string getTableName = "GetTableName";
            MethodInfo getTableNameMethod = typeof(SqlMapperExtensions).GetMethod(getTableName, BindingFlags.NonPublic | BindingFlags.Static);

            if (getTableNameMethod == null)
            {
                throw new ArgumentOutOfRangeException($"Method '{getTableName}' is not found in '{nameof(SqlMapperExtensions)}' class.");
            }

            string tableName = getTableNameMethod.Invoke(null, new object[] { type }) as string;

            return includeSchema
                ? tableName
                : tableName.TakeAfterFirst(".");
        }
    }
}
