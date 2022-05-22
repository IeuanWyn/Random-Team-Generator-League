using StackExchange.Redis;

namespace Managers
{
    static public class CacheManager
    {
        public static IDatabase redisCache { get; set; }

        public static string Get(string position)
        {
            return redisCache.StringGet(position);
        }

        public static void Set(string position, string value)
        {
            redisCache.StringSet(position, value);
        }




    }
}
