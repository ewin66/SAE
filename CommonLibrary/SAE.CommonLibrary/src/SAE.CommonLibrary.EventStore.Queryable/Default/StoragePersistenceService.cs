using SAE.CommonLibrary.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.EventStore.Queryable.Default
{
    /// <summary>
    /// 基于<seealso cref="IStorage"/><seealso cref="IPersistenceService"/>实现
    /// </summary>
    public class StoragePersistenceService : IPersistenceService
    {
        private readonly IStorage _storage;
        public StoragePersistenceService(IStorage storage)
        {
            this._storage = storage;
        }
        public void Add<T>(T t) where T : class
        {
            this._storage.Add(t);
        }

        public T Find<T>(string id) where T : class
        {
            return this._storage.Find<T>(id);
        }

        public void Remove<T>(T t) where T : class
        {
            this._storage.Remove(t);
        }

        public void Update<T>(T t) where T : class
        {
            this._storage.Update(t);
        }
    }
}
