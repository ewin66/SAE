using SAE.CommonLibrary.Json;
using SAE.CommonLibrary.Log;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SAE.CommonLibrary.Storage.MongoDB
{
    /// <summary>
    /// 
    /// </summary>
    public class MongoDBStorage : IStorage
    {
        

        #region Private Member
        private readonly Type _stringType = typeof(string);
        private readonly IJsonConvertor _jsonConvertor;
        private readonly IDictionary<Type, Delegate> _idDelegateStorage=new Dictionary<Type, Delegate>();
        private readonly MongoDBConfig _config;
        private readonly ILog _log;
        #endregion

        #region Protected Member
        /// <summary>
        /// 
        /// </summary>
        protected MongoDatabase MongoDb
        {
            get;
            private set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected MongoCollection<T> GetCollection<T>()
        {
            var colName = typeof(T).Name.ToLower();
            return MongoDb.GetCollection<T>(colName);
        }

        #endregion

        #region Ctor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonConvertor"></param>
        /// <param name="config"></param>
        /// <param name="log"></param>
        public MongoDBStorage(IJsonConvertor jsonConvertor,
                              MongoDBConfig config,
                              ILog<MongoDBStorage> log)
        {
            _log = log;
            _config = config;
            _jsonConvertor = jsonConvertor;
            var serverSetting = MongoServerSettings.FromUrl(new MongoUrl(this._config.Connection));
            this._log.Debug($"Connection={this._config.Connection},DB={this._config.DB}");
            this.MongoDb = new MongoServer(serverSetting).GetDatabase(this._config.DB);
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        public void Add<T>(T model)
        {
            if (model == null) return;
            this._log.Debug("Execute Add");
            this.GetCollection<T>().Insert(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        public void Update<T>(T model)
        {
            if (model == null) return;
            this._log.Debug("Execute Update");
            this.GetCollection<T>()
                .Save(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> AsQueryable<T>()
        {
            return this.GetCollection<T>().AsQueryable();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        public void Remove<T>(T model)
        {
            if (model == null) return;

            var id=IdentityDelegate(model);

            var query = new QueryDocument("_id",BsonValue.Create(id));

            var collection=this.GetCollection<T>();

            collection.Remove(query);

            this._log.Debug($"Remove {collection.Name}:{id}");
        }

        /// <summary>
        /// 标识表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        private object IdentityDelegate<T>(T model)
        {
            var type = typeof(T);
            Delegate @delegate;
            if (!_idDelegateStorage.TryGetValue(type, out @delegate))
            {
                _log.Debug("Identity Delegate:Get Id Property");
                var property = type.GetTypeInfo()
                                   .GetProperties()
                                   .Where(s => s.Name.ToLower() == "_id" || s.Name.ToLower() == "id")
                                   .FirstOrDefault();

                if (property == null)
                {
                    _log.Error("MongoDB Document Class You have to have a primary key");
                    throw new ArgumentNullException(nameof(model), $"{nameof(model)}中必须要有一个，唯一的键。默认为\"_id或\"\"id\"");
                }
                if (property.PropertyType.GetTypeInfo().IsValueType || property.PropertyType == _stringType)
                {
                    var p = Expression.Parameter(typeof(T));
                    var body = Expression.Property(p, property.Name);
                    var expression = Expression.Lambda(body, p);
                    @delegate= expression.Compile();
                    _idDelegateStorage[type] = @delegate;
                }
            }

            return @delegate.DynamicInvoke(model);
        }
    }
}
