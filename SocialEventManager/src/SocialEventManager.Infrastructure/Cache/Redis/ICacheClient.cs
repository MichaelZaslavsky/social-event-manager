using System;

namespace SocialEventManager.Infrastructure.Cache.Redis
{
    public interface ICacheClient
    {
        bool Delete<T>(string key);

        T Get<T>(string key);

        bool Set<T>(string key, T value, TimeSpan? expiry = null);
    }
}
