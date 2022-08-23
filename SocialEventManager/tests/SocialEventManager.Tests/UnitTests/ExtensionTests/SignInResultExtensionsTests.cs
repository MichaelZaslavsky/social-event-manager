using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Enums;
using SocialEventManager.Shared.Extensions;
using SocialEventManager.Tests.Common.DataMembers.Identity;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.ExtensionTests;

[UnitTest]
[Category(CategoryConstants.Extensions)]
public class SignInResultExtensionsTests
{
    [Theory]
    [MemberData(nameof(SignInResultData.SignInResultToUserLoginResult), MemberType = typeof(SignInResultData))]
    public void ToUserLoginResult_ReturnsCorrectUserLoginResult_WhenCalled(SignInResult signInResult, UserLoginResult expected)
    {
        UserLoginResult actual = signInResult.ToUserLoginResult();
        actual.Should().Be(expected);
    }
}
