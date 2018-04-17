using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAE.CommonLibrary.Storage.Memory
{
    /// <summary>
    /// 内存存储
    /// </summary>
    public class MemoryStorage : IStorage
    {
        private readonly Dictionary<Type,object> _storage;
        public MemoryStorage()
        {
            this._storage = new Dictionary<Type, object>();
        }
        public void Add<T>(T model)
        {
            this.GetStoreage<T>()
                .Add(model);
        }

        public IQueryable<T> AsQueryable<T>()
        {
            return this.GetStoreage<T>().AsQueryable();
        }

        public void Remove<T>(T model)
        {
            this.GetStoreage<T>()
                .Remove(model);
        }

        public void Update<T>(T model)
        {
            
        }

        private  List<T> GetStoreage<T>()
        {
            var type = typeof(T);
            object storage;
            if(!this._storage.TryGetValue(type,out storage))
            {
                storage = new List<T>();
                this._storage.Add(type, storage);
            }

            return storage as List<T>;
            
        }
    }
}
