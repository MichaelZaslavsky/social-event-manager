using Moq;
using SocialEventManager.DAL.Infrastructure;

namespace SocialEventManager.Tests.IntegrationTests.Infrastructure
{
    public static class InMemoryDatabaseExtensions
    {
        public static Mock<IDbSession> GetMockDbSession(this IInMemoryDatabase db)
        {
            var mock = new Mock<IDbSession>();

            mock.Setup(c => c.Connection).Returns(db.OpenConnectionAsync().GetAwaiter().GetResult());
            mock.Setup(c => c.Transaction).Returns(db.OpenTransaction());

            return mock;
        }
    }
}
