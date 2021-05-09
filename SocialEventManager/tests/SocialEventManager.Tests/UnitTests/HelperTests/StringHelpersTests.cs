using AutoFixture.Xunit2;
using SocialEventManager.Shared.Helpers;
using Xunit;

namespace SocialEventManager.Tests.UnitTests.HelperTests
{
    public class StringHelpersTests
    {
        [Theory]
        [InlineAutoData]
        public void GenerateRandomValue(int length)
        {
            string value = StringHelpers.GenerateRandomValue(length);
            Assert.Equal(length, value.Length);
        }
    }
}
