using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SAE.CommonLibrary.Cache
{
    /// <summary>
    /// 缓存扩展
    /// </summary>
    public static class CacheExtension
    {
        #region Delete
        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="cache">缓存实现</param>
        public static void Clear(this ICache cache)
        {
            cache.ClearAsync()
                 .Wait();
        }

        /// <summary>
        /// 根据<paramref name="keys"/>删除指定的缓存
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static bool Remove(this ICache cache,params string[] keys)
        {
            return cache.RemoveAsync(keys)
                        .GetAwaiter()
                        .GetResult();
        }
        #endregion

        #region Get
        /// <summary>
        /// 获得对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="cache">缓存实现</param>
        /// <returns></returns>
        public static T Get<T>(this ICache cache, string key)
        {
            return cache.GetAsync<T>(key)
                        .GetAwaiter()
                        .GetResult();
        }

        /// <summary>
        /// 获得更多的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">key集合</param>
        /// <param name="cache">缓存实现</param>
        /// <returns></returns>
        public static IEnumerable<T> GetMore<T>(this ICache cache, IEnumerable<string> keys)
        {
            return cache.GetMoreAsync<T>(keys)
                        .GetAwaiter()
                        .GetResult();
        }
     
        /// <summary>
        /// 获得所有键
        /// </summary>
        /// <returns>异步支持</returns>
        public static IEnumerable<string> GetKeys(this ICache cache)
        {
            return cache.GetKeysAsync()
                        .GetAwaiter()
                        .GetResult();
        }
        #endregion

        #region Add
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="kvs"></param>
        /// <param name="timeSpan"></param>
        /// <param name="cache">缓存实现</param>
        /// <returns></returns>
        public static bool Add<T>(this ICache cache, IDictionary<string, T> kvs, TimeSpan? timeSpan = default(TimeSpan?))
        {
            return cache.AddAsync(kvs, timeSpan)
                        .GetAwaiter()
                        .GetResult();
        }
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="kvs">键值对</param>
        /// <param name="dateTime">缓存时间</param>
        /// <param name="cache">缓存实现</param>
        /// <returns></returns>
        public static bool Add<T>(this ICache cache, IDictionary<string, T> kvs, DateTime dateTime)
        {
            return cache.AddAsync(kvs, dateTime)
                        .GetAwaiter()
                        .GetResult();
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="kvs">键值对</param>
        /// <param name="dateTime">缓存时间</param>
        /// <param name="cache">缓存实现</param>
        /// <returns></returns>
        public static Task<bool> AddAsync<T>(this ICache cache, IDictionary<string, T> kvs, DateTime dateTime)
        {
            return cache.AddAsync(kvs, Verify(dateTime));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="dateTime">过期日期</param>
        /// <param name="cache">缓存实现</param>
        /// <returns></returns>
        public static bool Add<T>(this ICache cache, string key, T value, DateTime dateTime)
        {
            return cache.AddAsync(key, value, dateTime)
                        .GetAwaiter()
                        .GetResult();
        }


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="dateTime">过期日期</param>
        /// <param name="cache">缓存实现</param>
        /// <returns></returns>
        public static Task<bool> AddAsync<T>(this ICache cache, string key, T value, DateTime dateTime)
        {
            return cache.AddAsync(key, value, Verify(dateTime));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="timeSpan">超时时间</param>
        /// <param name="cache">缓存实现</param>
        /// <returns></returns>
        public static bool Add<T>(this ICache cache, string key, T value, TimeSpan? timeSpan = default(TimeSpan?))
        {
            return cache.AddAsync(key, value, timeSpan)
                        .GetAwaiter()
                        .GetResult();
        } 
        #endregion

        /// <summary>
        /// 验证<paramref name="dateTime"/>的有效性，如果<paramref name="dateTime"/>无效，则强制转换成有效的时间差
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private static TimeSpan? Verify(DateTime dateTime)
        {
            var currentDate = DateTime.Now;
            TimeSpan? timeSpan;
            if (dateTime > currentDate)
            {
                timeSpan = dateTime - currentDate;
            }
            else if (currentDate > dateTime)
            {
                timeSpan = currentDate - dateTime;
            }
            else
            {
                timeSpan = null;
            }

            return timeSpan;
        }
    }
}
