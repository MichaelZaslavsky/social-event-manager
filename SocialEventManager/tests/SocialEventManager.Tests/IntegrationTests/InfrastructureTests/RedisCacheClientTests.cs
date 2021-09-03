using AutoFixture.Xunit2;
using FluentAssertions;
using SocialEventManager.Infrastructure.Cache.Redis;
using Xunit;

namespace SocialEventManager.Tests.IntegrationTests.InfrastructureTests
{
    public class RedisCacheClientTests
    {
        private readonly ICacheClient _cache;

        public RedisCacheClientTests(ICacheClient cache)
        {
            _cache = cache;
        }

        [Theory]
        [InlineAutoData]
        public void RedisCache_Set_Get_Delete_Should_Succeed(string key, string obj)
        {
            bool isSetSucceeded = _cache.Set(key, obj);
            isSetSucceeded.Should().BeTrue();

            object objFromCache = _cache.Get<object>(key);
            objFromCache.Should().NotBeNull().And.Be(obj);

            bool isDeleteSucceeded = _cache.Delete<object>(key);
            isDeleteSucceeded.Should().BeTrue();
        }

        [Theory]
        [InlineAutoData]
        public void RedisCache_Get_Should_Return_Null(string key)
        {
            object objFromCache = _cache.Get<object>(key);
            objFromCache.Should().BeNull();
        }
    }
}
