using System.Reflection;
using Dapper.Contrib.Extensions;
using FluentAssertions;
using SocialEventManager.Shared.Common.Constants;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Shared.Helpers;
using Xunit;

namespace SocialEventManager.Tests.UnitTests.ExtensionTests;

public class TypeExtensionsTests
{
    [Fact]
    public void GetNonPublicStaticMethod_Should_ReturnMethod_When_MethodNameExists()
    {
        const string methodName = "GetTableName";
        MethodInfo getTableNameMethod = typeof(SqlMapperExtensions).GetNonPublicStaticMethod(methodName);

        getTableNameMethod.Should().NotBeNull();
    }

    [Fact]
    public void GetNonPublicStaticMethod_Should_ReturnArgumentOutOfRangeException_When_MethodNameDoesNotExist()
    {
        string methodName = RandomGeneratorHelpers.GenerateRandomValue();
        Type type = typeof(SqlMapperExtensions);

        Action action = () => type.GetNonPublicStaticMethod(methodName);
        action.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage($"{ExceptionConstants.MethodIsNotFound(methodName, nameof(type))} (Parameter '{methodName}')");
    }
}
