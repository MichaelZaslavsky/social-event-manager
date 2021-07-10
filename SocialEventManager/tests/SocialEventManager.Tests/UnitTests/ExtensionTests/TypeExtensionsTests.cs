using System;
using System.Reflection;
using Dapper.Contrib.Extensions;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Helpers;
using SocialEventManager.Tests.Common.Constants;
using Xunit;

namespace SocialEventManager.Tests.UnitTests.ExtensionTests
{
    public class TypeExtensionsTests
    {
        [Fact]
        public void GetNonPublicStaticMethod_ShouldReturnMethod()
        {
            const string methodName = "GetTableName";
            MethodInfo getTableNameMethod = typeof(SqlMapperExtensions).GetNonPublicStaticMethod(methodName);

            Assert.NotNull(getTableNameMethod);
        }

        [Fact]
        public void GetNonPublicStaticMethod_ShouldReturnException()
        {
            string methodName = RandomGeneratorHelpers.GenerateRandomValue();
            Type type = typeof(SqlMapperExtensions);

            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => type.GetNonPublicStaticMethod(methodName));
            Assert.Equal($"{ExceptionConstants.MethodIsNotFound(methodName, nameof(type))} (Parameter '{methodName}')", ex.Message);
        }
    }
}
