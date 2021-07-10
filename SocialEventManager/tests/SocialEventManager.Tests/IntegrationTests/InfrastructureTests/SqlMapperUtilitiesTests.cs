using System;
using Dapper.Contrib.Extensions;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.Shared.Constants;
using Xunit;

namespace SocialEventManager.Tests.IntegrationTests.InfrastructureTests
{
    public class SqlMapperUtilitiesTests
    {
        [Fact]
        public void GetTableName()
        {
            string tableName = SqlMapperUtilities.GetTableName<Role>();
            Assert.Equal(TableNameConstants.Roles, tableName);
        }

        [Theory]
        [InlineData(typeof(Account), TableNameConstants.Accounts)]
        [InlineData(typeof(Role), TableNameConstants.Roles)]
        [InlineData(typeof(UserRole), TableNameConstants.UserRoles)]
        [InlineData(typeof(UserClaim), TableNameConstants.UserClaims)]
        public void GetTableName_ByType(Type type, string expectedResult)
        {
            string actualTableName = SqlMapperUtilities.GetTableName(type);
            Assert.Equal(expectedResult, actualTableName);
        }

        [Theory]
        [InlineData(typeof(ClaimBase))]
        public void GetTableName_ByTableNameMapper(Type type)
        {
            SqlMapperExtensions.TableNameMapper = (type) => type.Name;

            string actualTableName = SqlMapperUtilities.GetTableName(type);
            Assert.Equal(type.Name, actualTableName);

            SqlMapperExtensions.TableNameMapper = null;
        }
    }
}
