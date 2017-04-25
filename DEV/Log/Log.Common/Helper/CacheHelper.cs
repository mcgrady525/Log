using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Log.Common.Helper
{
    /// <summary>
    /// HttpRuntime.Cache缓存封装
    /// </summary>
    public sealed class CacheHelper
    {
        #region Get
        /// <summary>
        /// 获取数据缓存
        /// </summary>
        /// <param name="key">键</param>
        public static object Get(string key)
        {
            return HttpRuntime.Cache[key];
        }
        #endregion

        #region Set
        /// <summary>
        /// 设置缓存，使用默认绝对过期时间
        /// 默认1天
        /// </summary>
        public static void Set(string key, object value)
        {
            Set(key, value, DateTime.UtcNow.AddDays(1));
        }

        /// <summary>
        /// 设置缓存，指定绝对过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireTime">绝对过期时间</param>
        public static void Set(string key, object value, DateTime expireTime)
        {
            HttpRuntime.Cache.Insert(key, value, null, expireTime, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
        }
        #endregion

        #region Remove
        /// <summary>
        /// 移除指定缓存
        /// </summary>
        public static void Remove(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public static void RemoveAll()
        {
            var cacheEnumerator = HttpRuntime.Cache.GetEnumerator();
            while (cacheEnumerator.MoveNext())
            {
                HttpRuntime.Cache.Remove(cacheEnumerator.Key.ToString());
            }
        }
        #endregion
    }
}
