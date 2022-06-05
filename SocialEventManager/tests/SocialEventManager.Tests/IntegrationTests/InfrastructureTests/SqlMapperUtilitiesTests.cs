using Dapper.Contrib.Extensions;
using FluentAssertions;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Shared.Constants;
using Xunit;

namespace SocialEventManager.Tests.IntegrationTests.InfrastructureTests;

public class SqlMapperUtilitiesTests
{
    [Fact]
    public void GetTableName_Should_ReturnTableName_When_ClassHasTableAttribute()
    {
        string tableName = SqlMapperUtilities.GetTableName<Role>();
        tableName.Should().Be(TableNameConstants.Roles);
    }

    [Theory]
    [InlineData(typeof(Account), TableNameConstants.Accounts)]
    [InlineData(typeof(Role), TableNameConstants.Roles)]
    [InlineData(typeof(UserRole), TableNameConstants.UserRoles)]
    [InlineData(typeof(UserClaim), TableNameConstants.UserClaims)]
    public void GetTableName_Should_ReturnTableName_When_TypeHasTableAttribute(Type type, string expectedTableName)
    {
        string actualTableName = SqlMapperUtilities.GetTableName(type);
        actualTableName.Should().Be(expectedTableName);
    }

    [Theory]
    [InlineData(typeof(ClaimBase))]
    public void GetTableName_Should_ReturnTableName_When_TypeIsDefinedInTableNameMapper(Type type)
    {
        SqlMapperExtensions.TableNameMapper = (type) => type.Name;

        string actualTableName = SqlMapperUtilities.GetTableName(type);
        actualTableName.Should().Be(type.Name);

        SqlMapperExtensions.TableNameMapper = null;
    }
}
