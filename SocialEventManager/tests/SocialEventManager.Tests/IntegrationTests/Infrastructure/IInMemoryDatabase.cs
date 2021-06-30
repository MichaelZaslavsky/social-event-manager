using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SocialEventManager.Tests.IntegrationTests.Infrastructure
{
    public interface IInMemoryDatabase
    {
        public Task<IDbConnection> OpenConnectionAsync();

        public IDbTransaction OpenTransaction();

        Task InsertAsync<T>(T item);

        Task InsertAsync<T>(IEnumerable<T> items);

        Task CreateTableIfNotExistsAsync<T>();

        Task CleanupAsync();
    }
}
