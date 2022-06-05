using Moq;
using SocialEventManager.DAL.Infrastructure;

namespace SocialEventManager.Tests.IntegrationTests.Infrastructure;

public class RepositoryTestBase<TIRepository, TEntity> : TestBase
    where TIRepository : class, IGenericRepository<TEntity>
    where TEntity : class
{
    protected RepositoryTestBase(IInMemoryDatabase db, TIRepository repository)
        : base(db)
    {
        Repository = repository;
        MockRepository = new Mock<TIRepository>();

        db.CreateRelevantTablesIfNotExistAsync<TEntity>().GetAwaiter().GetResult();
    }

    protected TIRepository Repository { get; }

    protected Mock<TIRepository> MockRepository { get; }
}
