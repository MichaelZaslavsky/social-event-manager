using AutoFixture.Xunit2;
using FluentAssertions;
using ServiceStack.Redis;
using Xunit;

namespace SocialEventManager.Tests.IntegrationTests.InfrastructureTests
{
    public class RedisCacheClientTests
    {
        private readonly IRedisClientsManagerAsync _manager;

        public RedisCacheClientTests(IRedisClientsManagerAsync manager)
        {
            _manager = manager;
        }

        [Theory]
        [InlineAutoData]
        public async Task RedisCache_Set_Get_Delete_Should_Succeed_When_DataIsValid(string key, string obj)
        {
            await using IRedisClientAsync cache = await _manager.GetClientAsync();

            bool isSetSucceeded = await cache.SetAsync(key, obj);
            isSetSucceeded.Should().BeTrue();

            object objFromCache = await cache.GetAsync<object>(key);
            objFromCache.Should().NotBeNull().And.Be(obj);

            bool isDeleteSucceeded = await cache.RemoveAsync(key);
            isDeleteSucceeded.Should().BeTrue();
        }

        [Theory]
        [InlineAutoData]
        public async Task RedisCache_Get_Should_ReturnNull_When_KeyNotExists(string key)
        {
            await using IRedisClientAsync cache = await _manager.GetClientAsync();

            object objFromCache = await cache.GetAsync<object>(key);
            objFromCache.Should().BeNull();
        }
    }
}
