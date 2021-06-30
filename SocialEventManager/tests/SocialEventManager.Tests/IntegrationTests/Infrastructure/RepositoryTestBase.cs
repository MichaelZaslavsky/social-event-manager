using Moq;
using SocialEventManager.DAL.Infrastructure;

namespace SocialEventManager.Tests.IntegrationTests.Infrastructure
{
    public class RepositoryTestBase<TInterface, TEntity> : TestBase
        where TInterface : class, IGenericRepository<TEntity>
        where TEntity : class
    {
        protected RepositoryTestBase(IInMemoryDatabase db, TInterface repository)
            : base(db)
        {
            Repository = repository;
            MockRepository = new Mock<TInterface>();

            db.CreateTableIfNotExistsAsync<TEntity>().GetAwaiter().GetResult();
        }

        protected TInterface Repository { get; }

        protected Mock<TInterface> MockRepository { get; }
    }
}
