using System.Net;
using FluentAssertions;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Tests.Common.DataMembers;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.ExtensionTests;

[UnitTest]
[Category(CategoryConstants.Extensions)]
public class ExceptionExtensionsTests
{
    [Theory]
    [MemberData(nameof(ExceptionData.ExceptionDataForHttpStatusAndTitle), MemberType = typeof(ExceptionData))]
    public void ToHttpStatusCodeAndTitle_Should_ReturnCorrectResult_WhenCalled(Exception ex, (HttpStatusCode, string) expectedResult)
    {
        (HttpStatusCode, string) actualResult = ex.ToHttpStatusCodeAndTitle();
        actualResult.Should().Be(expectedResult);
    }

    [Theory]
    [MemberData(nameof(ExceptionData.CriticalExceptionsData), MemberType = typeof(ExceptionData))]
    public void IsCritical_Should_ReturnCorrectResult_WhenCalled(Exception ex, bool expectedResult)
    {
        bool actualResult = ex.IsCritical();
        actualResult.Should().Be(expectedResult);
    }
}
