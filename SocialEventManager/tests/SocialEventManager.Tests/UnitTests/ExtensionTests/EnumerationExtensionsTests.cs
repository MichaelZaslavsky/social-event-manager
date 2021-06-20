using System;
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
        public void GetDescription(Enum value, string expectedResult)
        {
            string actualResult = value.GetDescription();
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
