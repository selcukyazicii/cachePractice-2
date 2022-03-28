using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cachePractice_2.Controllers
{
    public class HomeController : Controller
    {
        const string cacheKey = "cacheKeyValue";
        private readonly IMemoryCache _memoryCache;
        public HomeController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public IActionResult Index()
        {
            DateTime cacheEntry;
            if (!_memoryCache.TryGetValue(cacheKey,out cacheEntry))
            {
                cacheEntry = DateTime.Now;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30)) 
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(1)) 
                    .SetPriority(CacheItemPriority.Normal); 
                _memoryCache.Set(cacheKey, cacheEntry, cacheEntryOptions);
            }
            return View(cacheEntry);
        }
    }
}
