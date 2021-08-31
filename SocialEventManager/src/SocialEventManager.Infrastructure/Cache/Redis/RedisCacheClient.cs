using System;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;

namespace SocialEventManager.Infrastructure.Cache.Redis
{
    public class RedisCacheClient : ICacheClient
    {
        private readonly IRedisClientsManager _redisClientsManager;
        private static readonly TimeSpan DefaultExpiry = TimeSpan.FromMinutes(1);

        public RedisCacheClient(IRedisClientsManager redisClientsManager)
        {
            _redisClientsManager = redisClientsManager;
        }

        public bool Set<T>(string key, T value, TimeSpan? expiry = null)
        {
            using IRedisClient redis = _redisClientsManager.GetClient();
            IRedisTypedClient<T> item = redis.As<T>();
            item.SetValue(key, value, expiry ?? DefaultExpiry);

            return true;
        }

        public T Get<T>(string key)
        {
            using IRedisClient redis = _redisClientsManager.GetClient();
            IRedisTypedClient<T> item = redis.As<T>();

            return item.GetValue(key);
        }

        public bool Delete<T>(string key)
        {
            using IRedisClient redis = _redisClientsManager.GetClient();
            IRedisTypedClient<T> item = redis.As<T>();
            item.RemoveEntry(key);

            return true;
        }
    }
}
