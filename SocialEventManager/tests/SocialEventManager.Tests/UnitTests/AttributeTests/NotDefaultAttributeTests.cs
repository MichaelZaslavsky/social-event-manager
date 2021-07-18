using FluentAssertions;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Utilities.Attributes;
using SocialEventManager.Tests.Common.DataMembers;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.AttributeTests
{
    [UnitTest]
    [Category(CategoryConstants.Attributes)]
    public class NotDefaultAttributeTests
    {
        [Theory]
        [InlineData(null, true)]
        [InlineData(1, true)]
        [InlineData(DataConstants.RandomText, true)]
        [InlineData(new int[] { 1 }, true)]
        [InlineData(0, false)]
        [MemberData(nameof(GuidData.NotDefaultGuidData), MemberType = typeof(GuidData))]
        public void IsValid_Should_Return_Expected_Result(object value, bool expectedResult)
        {
            var notDefaultAttribute = new NotDefaultAttribute();
            bool actualResult = notDefaultAttribute.IsValid(value);

            actualResult.Should().Be(expectedResult);
        }
    }
}
