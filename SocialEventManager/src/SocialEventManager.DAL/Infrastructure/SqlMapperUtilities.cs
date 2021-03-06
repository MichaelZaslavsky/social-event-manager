using System.Reflection;
using Dapper.Contrib.Extensions;
using SocialEventManager.Shared.Extensions;

namespace SocialEventManager.DAL.Infrastructure;

public static class SqlMapperUtilities
{
    public static string GetTableName<T>() =>
        GetTableName(typeof(T));

    public static string GetTableName(Type type)
    {
        if (SqlMapperExtensions.TableNameMapper is not null)
        {
            return SqlMapperExtensions.TableNameMapper(type);
        }

        const string methodName = "GetTableName";
        MethodInfo getTableNameMethod = typeof(SqlMapperExtensions).GetNonPublicStaticMethod(methodName);

        string? tableName = getTableNameMethod.Invoke(null, new object[] { type }) as string;
        ArgumentNullException.ThrowIfNull(tableName);

        return tableName;
    }
}
