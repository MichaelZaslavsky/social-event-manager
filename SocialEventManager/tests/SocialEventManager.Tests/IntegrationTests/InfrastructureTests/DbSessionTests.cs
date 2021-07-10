using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers;
using SocialEventManager.Tests.Common.Constants;
using Xunit;

namespace SocialEventManager.Tests.IntegrationTests.InfrastructureTests
{
    public class DbSessionTests
    {
        private static IConfiguration _configuration;

        public DbSessionTests(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Fact]
        public void InitDbSession_OpenConnection()
        {
            string connectionString = _configuration.GetConnectionString(DbConstants.SocialEventManagerTest);
            var session = new DbSession(connectionString);

            Assert.NotNull(session);
            Assert.NotNull(session.Connection);
            Assert.Equal(connectionString, session.Connection.ConnectionString);
            Assert.Null(session.Transaction);
        }

        [Fact]
        public void InitDbSession_OpenConnection_Fail()
        {
            string connectionString = RandomGeneratorHelpers.GenerateRandomValue();
            ArgumentException ex = Assert.Throws<ArgumentException>(() => new DbSession(connectionString));
            Assert.Equal(ExceptionConstants.InvalidConnectionString, ex.Message);
        }

        [Fact]
        public void InitDbSession_OpenTransaction()
        {
            string connectionString = _configuration.GetConnectionString(DbConstants.SocialEventManagerTest);
            var session = new DbSession(connectionString);
            using IDbTransaction transaction = session.Connection.BeginTransaction();
            Assert.NotNull(transaction);
        }
    }
}
