using Microsoft.AspNetCore.Mvc;
using ServiceStack.Caching;

namespace SocialEventManager.API.Controllers
{
    // Temporary API for testing the Redis caching mechanism.
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : Controller
    {
        private readonly ICacheClient _cache;

        public RedisController(ICacheClient cache)
        {
            _cache = cache;
        }

        [HttpGet("get/{key}")]
        public IActionResult GetCachedItem(string key)
        {
            CachedItem result = _cache.Get<CachedItem>(key);
            return Ok(result);
        }

        [HttpPost("set/{key}")]
        public IActionResult SetCachedItem(string key, [FromBody] CachedItem item)
        {
            var result = _cache.Set(key, item);

            return Ok(result);
        }
    }

#pragma warning disable SA1402 // File may only contain a single type
    public class CachedItem
#pragma warning restore SA1402 // File may only contain a single type
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}
