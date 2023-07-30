using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureSample.Utilities.Constants;

namespace CleanArchitectureSample.Utilities.Cache
{
    public partial class CacheManager
    {
        #region codes
        public static void AddOrUpdateCodes(string category, object cacheValue)
        {
            string key = string.Format(CacheKeys.CodeCategoryList, category);
            CacheValues.AddOrUpdate(key, cacheValue, (a, b) => { return cacheValue; });
        }
        public static bool TryGetCodes<TData>(string category, out TData data)
        {
            string key = string.Format(CacheKeys.CodeCategoryList, category);
            return TryGet<TData>(key, out data);
        }
        public static bool RemoveCodes(string category)
        {
            string key = string.Format(CacheKeys.CodeCategoryList, category);
            return RemoveCache(key);
        }
        #endregion
        #region codeitem
        public static void AddOrUpdateCodeItem(string category, string name, object cacheValue)
        {
            string key = string.Format(CacheKeys.CodeItem, category, name);
            CacheValues.AddOrUpdate(key, cacheValue, (a, b) => { return cacheValue; });
        }
        public static bool TryGetCodeItem<TData>(string category, string name, out TData data)
        {
            string key = string.Format(CacheKeys.CodeItem, category, name);
            return TryGet<TData>(key, out data);
        }
        public static bool RemoveCodeItem(string category, string name)
        {
            string key = string.Format(CacheKeys.CodeItem, category, name);
            return RemoveCache(key);
        }
        #endregion
    }
}
