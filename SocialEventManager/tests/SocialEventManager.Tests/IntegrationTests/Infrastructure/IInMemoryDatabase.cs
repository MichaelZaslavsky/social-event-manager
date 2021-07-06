using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SocialEventManager.Tests.IntegrationTests.Infrastructure
{
    public interface IInMemoryDatabase
    {
        public Task<IDbConnection> OpenConnectionAsync();

        public IDbTransaction OpenTransaction();

        Task<int> InsertAsync<T>(T item);

        Task InsertAsync<T>(IEnumerable<T> items);

        Task DeleteAsync<T>(T item);

        Task<IEnumerable<T>> SelectAsync<T>();

        Task<IEnumerable<T>> WhereAsync<T>(string columnName, object value);

        Task<T> SingleWhereAsync<T>(string columnName, object value);

        Task CreateTableIfNotExistsAsync<T>();

        Task CreateTableIfNotExistsAsync(Type type);

        Task CreateRelevantTablesIfNotExistAsync<T>();

        Task CleanupAsync();
    }
}
