using System.Data;
using System.Reflection;
using Dapper.Contrib.Extensions;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Converters;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Helpers.Queries;

namespace SocialEventManager.Tests.IntegrationTests.Infrastructure;

public class InMemoryDatabase : IInMemoryDatabase
{
    private readonly OrmLiteConnectionFactory _dbFactory;

    public InMemoryDatabase(string connectionString)
    {
        _dbFactory = new(connectionString, SqlServerDialect.Provider);

        StringConverter converter = OrmLiteConfig.DialectProvider.GetStringConverter();
        converter.UseUnicode = true;
    }

    public async Task<IDbConnection> OpenConnectionAsync() =>
        await _dbFactory.OpenDbConnectionAsync(CancellationToken.None);

    public IDbTransaction OpenTransaction() => _dbFactory.AlwaysReturnTransaction;

    public async Task<int> InsertAsync<T>(T item)
    {
        using IDbConnection db = await OpenConnectionAsync();
        return (int)await db.InsertAsync(item);
    }

    public async Task InsertAsync<T>(IEnumerable<T> items)
    {
        using IDbConnection db = await OpenConnectionAsync();

        foreach (T item in items)
        {
            await db.InsertAsync(item);
        }
    }

    public async Task DeleteAsync<T>(T item)
    {
        using IDbConnection db = await OpenConnectionAsync();
        await db.DeleteAsync(item);
    }

    public async Task<IEnumerable<T>> SelectAsync<T>()
    {
        using IDbConnection db = await OpenConnectionAsync();
        return (await db.SelectAsync<T>()).AsEnumerable();
    }

    public async Task<IEnumerable<T>> WhereAsync<T>(string columnName, object value)
    {
        using IDbConnection db = await OpenConnectionAsync();
        return (await db.WhereAsync<T>(columnName, value)).AsEnumerable();
    }

    public async Task<T> SingleWhereAsync<T>(string columnName, object value)
    {
        using IDbConnection db = await OpenConnectionAsync();
        return await db.SingleWhereAsync<T>(columnName, value);
    }

    public async Task CreateTableIfNotExistsAsync<T>() =>
        await CreateTableIfNotExistsAsync(typeof(T));

    public async Task CreateTableIfNotExistsAsync(Type type)
    {
        using IDbConnection db = await OpenConnectionAsync();
        db.CreateTableIfNotExists(type);
    }

    public async Task CreateRelevantTablesIfNotExistAsync<T>() =>
        await CreateRelevantTablesIfNotExistAsync(typeof(T));

    public async Task CleanupAsync()
    {
        using IDbConnection db = await OpenConnectionAsync();
        await db.ExecuteNonQueryAsync(TableQueryHelpers.SafelyDropAllTables());

        return;
    }

    #region Private Methods

    private async Task CreateRelevantTablesIfNotExistAsync(Type? type)
    {
        if (type is null)
        {
            return;
        }

        IEnumerable<PropertyInfo> properties = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(ForeignKeyAttribute)));

        if (properties.IsEmpty())
        {
            await CreateTableIfNotExistsAsync(type);
            return;
        }

        foreach (PropertyInfo property in properties)
        {
            Type? foreignKeyType = property.GetCustomAttribute<ForeignKeyAttribute>()?.Type;
            await CreateRelevantTablesIfNotExistAsync(foreignKeyType);
        }

        await CreateTableIfNotExistsAsync(type);
        return;
    }

    #endregion Private Methods
}
