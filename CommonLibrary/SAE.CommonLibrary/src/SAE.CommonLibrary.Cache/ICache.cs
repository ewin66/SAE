using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE.CommonLibrary.Cache
{
    /// <summary>
    /// 缓存
    /// </summary>
    public interface ICache:IDisposable
    {

        /// <summary>
        /// 清空
        /// </summary>
        Task ClearAsync();
        /// <summary>
        /// 添加（异步支持）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="timeSpan">超时时间</param>
        /// <returns></returns>
        Task<bool> AddAsync<T>(string key, T value, TimeSpan? timeSpan = null);

        /// <summary>
        /// 批量添加（异步支持）
        /// </summary>
        /// <param name="kvs"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        Task<bool> AddAsync<T>(IDictionary<string, T> kvs, TimeSpan? timeSpan = null);
        /// <summary>
        /// 删除（异步支持）
        /// </summary>
        /// <param name="keys">最少传递一个key</param>
        /// <returns></returns>
        Task<bool> RemoveAsync(params string[] keys);
        /// <summary>
        /// 获得对象(异步支持)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key);
        /// <summary>
        /// 获得更多的对象(异步支持)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">key集合</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetMoreAsync<T>(IEnumerable<string> keys);
        /// <summary>
        /// 获得所有键
        /// </summary>
        /// <returns>异步支持</returns>
        Task<IEnumerable<string>> GetKeysAsync();
    }
}
