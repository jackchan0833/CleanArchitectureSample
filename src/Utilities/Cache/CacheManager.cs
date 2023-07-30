using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanArchitectureSample.Utilities.Cache
{
    public partial class CacheManager
    {
        private static ConcurrentDictionary<string, object> CacheValues = new ConcurrentDictionary<string, object>();
        public static void AddOrUpdate(string key, object cacheValue)
        {
            CacheValues.AddOrUpdate(key, cacheValue, (a,b) => { return cacheValue; });
        }
        public static bool TryGet<TData>(string key, out TData data)
        {
            if (CacheValues.TryGetValue(key, out object objValue))
            {
                data = (TData)objValue;
                return true;
            }
            data = default(TData);
            return false;
        }
        public static bool RemoveCache(string key)
        {
            return CacheValues.Remove(key, out object val);
        }
        public static void ClearAll()
        {
            CacheValues.Clear();
        }
    }
}
