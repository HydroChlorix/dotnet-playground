using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Playground.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SampleCacheController : ControllerBase
{
    private readonly IDistributedCache _distributedCache;
    private readonly ILogger<SampleCacheController> _logger;

    public SampleCacheController(IDistributedCache distributedCache , ILogger<SampleCacheController> logger)
    {
        _distributedCache = distributedCache;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        string cacheKey = "sampleKey";
        string cachedValue = await _distributedCache.GetStringAsync(cacheKey);

        if (string.IsNullOrEmpty(cachedValue))
        {
            // Key not in cache, so get data.
            cachedValue = "This is a cached value.";

            // Set cache options.
            var cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));

            // Save data in cache.
            await _distributedCache.SetStringAsync(cacheKey, cachedValue, cacheEntryOptions);

            _logger.LogInformation($"Init : {cacheKey} {cachedValue}");
        }

        return Ok(cachedValue);
    }
}
