using System;
using System.Data;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Shared.Common.Constants;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Helpers;
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
        public void InitDbSession_OpenConnection_Should_Return_Valid_Session()
        {
            string connectionString = _configuration.GetConnectionString(DbConstants.SocialEventManagerTest);
            DbSession session = new(connectionString);

            session.Should().NotBeNull();
            session.Connection.Should().NotBeNull();
            session.Connection.ConnectionString.TakeUntilLast(DataConstants.Password)
                .Should().Be(connectionString.TakeUntilLast(DataConstants.Password) + DataConstants.MultipleActiveResultSetsTrue);

            session.Transaction.Should().BeNull();
        }

        [Fact]
        public void InitDbSession_OpenConnection_Should_Return_Exception()
        {
            string connectionString = RandomGeneratorHelpers.GenerateRandomValue();
            Action action = () => new DbSession(connectionString);
            action.Should().Throw<ArgumentException>().WithMessage(ExceptionConstants.InvalidConnectionString);
        }

        [Fact]
        public void InitDbSession_OpenTransaction_Should_Return_Transaction()
        {
            string connectionString = _configuration.GetConnectionString(DbConstants.SocialEventManagerTest);
            DbSession session = new(connectionString);
            using IDbTransaction transaction = session.Connection.BeginTransaction();

            transaction.Should().NotBeNull();
        }

        [Fact]
        public void InitDbSession_DisposeTransaction_Should_Return_Null_Connection()
        {
            string connectionString = _configuration.GetConnectionString(DbConstants.SocialEventManagerTest);
            DbSession session = new(connectionString);
            using IDbTransaction transaction = session.Connection.BeginTransaction();
            session.Dispose();

            transaction.Connection.Should().BeNull();
        }
    }
}
