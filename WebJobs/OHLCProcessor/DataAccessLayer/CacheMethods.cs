using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace DataAccessLayer
{
    public class CacheMethods
    {
        //private readonly IConfigSettings _configSettings;
        //public CacheMethods(IConfigSettings configSettings)
        //{
        //    _configSettings = configSettings;
        //}

        private static string CacheConnectionString => "aztgcache.redis.cache.windows.net:6380,password=SZMbjgGgbAIsKTQywXr10CG9lCS6o+rarkqWD5GvLRA=,ssl=True,abortConnect=False";

        Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect(CacheConnectionString);
        });

        public ConnectionMultiplexer Connection => lazyConnection.Value;
    }
}
