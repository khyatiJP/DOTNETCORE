using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theatre.Infrastructure.Provider
{
    public class RedisProvider
    {
        private readonly IDistributedCache _redisCache;

        public RedisProvider(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public bool SetString(string key, string Value)
        {
            _redisCache.SetString(key, Value);
            return true;
        }
        public string GetString(string key)
        {
            return _redisCache.GetString(key);
            
        }
    }
}
