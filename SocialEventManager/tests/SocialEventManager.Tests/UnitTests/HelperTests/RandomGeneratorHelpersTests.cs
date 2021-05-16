using AutoFixture.Xunit2;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.HelperTests
{
    [UnitTest]
    [Category(CategoryConstants.Helpers)]
    public class RandomGeneratorHelpersTests
    {
        [Theory]
        [InlineAutoData]
        public void GenerateRandomValue(int length)
        {
            string value = RandomGeneratorHelpers.GenerateRandomValue(length);
            Assert.Equal(length, value.Length);
        }

        [Fact]
        public void NextInt32() =>
            RandomGeneratorHelpers.NextInt32();
    }
}
