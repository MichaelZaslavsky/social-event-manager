using System;
using FluentAssertions;
using SocialEventManager.DAL.Enums;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.ExtensionTests
{
    [UnitTest]
    [Category(CategoryConstants.Extensions)]
    public class EnumerationExtensionsTests
    {
        [Theory]
        [InlineData(RoleType.Admin, "Admin")]
        [InlineData(RoleType.User, "User")]
        [InlineData((RoleType)(-1), "-1")]
        public void GetDescription_Should_Return_Description(Enum value, string expectedDescription)
        {
            string actualResult = value.GetDescription();
            actualResult.Should().Be(expectedDescription);
        }
    }
}
