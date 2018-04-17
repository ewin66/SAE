using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SAE.CommonLibrary.Json;
using StackExchange.Redis;
using SAE.CommonLibrary.Log;
using System.Diagnostics;

namespace SAE.CommonLibrary.Cache.Redis
{
    /// <summary>
    /// ICache的Redis缓存实现
    /// </summary>
    public class RedisCache : ICache
    {
        #region Private Field
        /// <summary>
        /// 链接实例
        /// </summary>
        private readonly Lazy<ConnectionMultiplexer> _connection;
        /// <summary>
        /// Json转换器
        /// </summary>
        private readonly IJsonConvertor _jsonConvertor;

        /// <summary>
        /// 配置文件
        /// </summary>
        private readonly RedisConfig _config;
        /// <summary>
        /// 
        /// </summary>
        private readonly ILog _log; 
        #endregion

        #region Ctor
        /// <summary>
        /// redis缓存
        /// </summary>
        /// <param name="config">redis配置</param>
        /// <param name="jsonConvertor">json转换器</param>
        /// <param name="log">log日志器</param>
        public RedisCache(RedisConfig config, IJsonConvertor jsonConvertor, ILog<RedisCache> log)
        {
            _log = log;
            _config = config;
            _jsonConvertor = jsonConvertor;
            _connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(this._config.Connection));

        }
        ~RedisCache()
        {
            this.Dispose();
        }
        #endregion

        #region Private Method
        private T Deserialize<T>(string input)
        {
            return string.IsNullOrWhiteSpace(input) ?
                         default(T) :
                         _jsonConvertor.Deserialize<T>(input);
        }
        /// <summary>
        /// 获得缓存数据库
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        private async Task GetDataBase(Func<IDatabase, Task> db)
        {
            //using (var conn = await this._connection())
            //{

            _log.Debug($"Get DB:{this._config.DB}");
            if (this._connection.Value.IsConnected)
            {
                await db(this._connection.Value.GetDatabase(this._config.DB));
                _log.Debug($"Connection DB:{this._config.DB}");
            }

            //}
        } 
        #endregion

        #region ICache Method
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="kvs"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public Task<bool> AddAsync<T>(IDictionary<string, T> kvs, TimeSpan? timeSpan = default(TimeSpan?))
        {
            var result = true;
            return Task.Run(async () =>
            {
                await this.GetDataBase(async db =>
                {
                    foreach (var kv in kvs)
                    {
                        _log.Debug($"添加:{kv.Key}");
                        result = await db.StringSetAsync(kv.Key,
                                         _jsonConvertor.Serialize(kv.Value),
                                         timeSpan);
                        if (!result)
                            break;
                    }
                });

            }).ContinueWith(t =>
            {
                return result;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public Task<bool> AddAsync<T>(string key, T value, TimeSpan? timeSpan = default(TimeSpan?))
        {
            var result = false;

            return this.GetDataBase(async db =>
            {
                _log.Debug($"Add Async:{key},timeSpan:{timeSpan}");
                result = await db.StringSetAsync(key, _jsonConvertor.Serialize(value), timeSpan);
            }).ContinueWith(t => result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<T> GetAsync<T>(string key)
        {
            T result = default(T);
            return this.GetDataBase(async db =>
            {
                var value = await db.StringGetAsync(key);

                _log.Debug($"Get Async:{key}");

                if (value.HasValue)
                {
                    result = _jsonConvertor.Deserialize<T>(value.ToString());
                }
            }).ContinueWith(t => result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetKeysAsync()
        {
            IEnumerable<string> keys = new List<string>();
            return await this.GetDataBase(db =>
            {
                _log.Debug($"Get Keys Async");

                keys = db.Multiplexer
                         .GetServer(this._config.ElectServer())
                         .Keys(this._config.DB)
                         .Select(s => s.ToString())
                         .ToList();
                return Task.CompletedTask;
            }).ContinueWith(t => keys);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetMoreAsync<T>(IEnumerable<string> keys)
        {
            List<T> list = new List<T>();

            IEnumerable<string> listStr = new List<string>();
            if (keys == null || !keys.Any())
            {
                return Task.FromResult((IEnumerable<T>)list);
            }
            return this.GetDataBase(async db =>
            {
                var redisValues = await db.StringGetAsync(keys.Select(s => (RedisKey)s)
                                          .ToArray());
                listStr = redisValues.Select(s => s.ToString());

            }).ContinueWith(t =>
            {
                foreach (var str in listStr)
                {
                    list.Add(this.Deserialize<T>(str));
                }
                return (IEnumerable<T>)list;
            });


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public Task<bool> RemoveAsync(params string[] keys)
        {

            var result = false;
            if (keys == null || !keys.Any())
            {
                return Task.FromResult(result);
            }
            return this.GetDataBase(async db =>
            {
                this._log.Info($"移除键值:{keys.Aggregate((left, right) => left + "," + right)}");
                var count = await db.KeyDeleteAsync(keys.Select(s => (RedisKey)s).ToArray());
                result = count > 0;
            }).ContinueWith(t => result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task ClearAsync()
        {
            await this.GetDataBase(async db =>
            {
                _log.Warn($"Clear:清空服务器\"{this._config.ElectServer()}\",第{this._config.DB}数据库的所有缓存");
                await db.Multiplexer.GetServer(this._config.ElectServer())
                        .FlushDatabaseAsync(this._config.DB);
            });
        } 
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~RedisCache() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (!disposedValue)
            {
                this._connection.Value.Dispose();

                disposedValue = true;
            }
            GC.SuppressFinalize(this);
        }
        #endregion
        
    }
}
