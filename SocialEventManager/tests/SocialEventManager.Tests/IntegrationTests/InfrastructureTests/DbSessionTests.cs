using System.Data;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Shared.Common.Constants;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Helpers;
using Xunit;

namespace SocialEventManager.Tests.IntegrationTests.InfrastructureTests;

public class DbSessionTests
{
    private readonly IConfiguration _configuration;

    public DbSessionTests(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [Fact]
    public void InitDbSession_Should_ReturnValidSession_When_ConnectionStringIsValid()
    {
        string connectionString = _configuration.GetConnectionString(DbConstants.SocialEventManagerTest);
        DbSession session = new(connectionString);

        session.Should().NotBeNull();
        session.Connection.Should().NotBeNull();
        session.Connection.ConnectionString.TakeUntilFirst(DataConstants.UserId).Should().Be(connectionString.TakeUntilFirst(DataConstants.UserId));

        session.Transaction.Should().BeNull();
    }

    [Fact]
    public void InitDbSession_Should_ThrowArgumentException_When_ConnectionStringIsInvalid()
    {
        string connectionString = RandomGeneratorHelpers.GenerateRandomValue();
        Action action = () => new DbSession(connectionString);
        action.Should().Throw<ArgumentException>().WithMessage(ExceptionConstants.InvalidConnectionString);
    }

    [Fact]
    public void InitDbSession_Should_ReturnTransaction_When_TransactionHasBegun()
    {
        string connectionString = _configuration.GetConnectionString(DbConstants.SocialEventManagerTest);
        DbSession session = new(connectionString);
        using IDbTransaction transaction = session.Connection.BeginTransaction();

        transaction.Should().NotBeNull();
    }

    [Fact]
    public void InitDbSession_Should_ReturnNullConnection_When_ConnectionIsDisposed()
    {
        string connectionString = _configuration.GetConnectionString(DbConstants.SocialEventManagerTest);
        DbSession session = new(connectionString);
        using IDbTransaction transaction = session.Connection.BeginTransaction();
        session.Dispose();

        transaction.Connection.Should().BeNull();
    }
}
