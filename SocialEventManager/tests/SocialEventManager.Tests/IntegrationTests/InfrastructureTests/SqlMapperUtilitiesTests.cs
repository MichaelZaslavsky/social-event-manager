using Dapper.Contrib.Extensions;
using FluentAssertions;
using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Entities;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.IntegrationTests.InfrastructureTests;

[IntegrationTest]
[Category(CategoryConstants.Infrastructure)]
public sealed class SqlMapperUtilitiesTests
{
    [Fact]
    public void GetTableName_Should_ReturnTableName_When_ClassHasTableAttribute()
    {
        string tableName = SqlMapperUtilities.GetTableName<Role>();
        tableName.Should().Be(TableNameConstants.Roles);
    }

    [Theory]
    [InlineData(typeof(Role), TableNameConstants.Roles)]
    public void GetTableName_Should_ReturnTableName_When_TypeHasTableAttribute(Type type, string expectedTableName)
    {
        string actualTableName = SqlMapperUtilities.GetTableName(type);
        actualTableName.Should().Be(expectedTableName);
    }

    [Theory]
    [InlineData(typeof(Role))]
    public void GetTableName_Should_ReturnTableName_When_TypeIsDefinedInTableNameMapper(Type type)
    {
        SqlMapperExtensions.TableNameMapper = (type) => type.Name;

        string actualTableName = SqlMapperUtilities.GetTableName(type);
        actualTableName.Should().Be(type.Name);

        SqlMapperExtensions.TableNameMapper = null;
    }
}
