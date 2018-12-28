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
            dynamic @dynamic = model;
            this.GetStoreage<T>()
                .Add(@dynamic.Id,model);
        }

        public IQueryable<T> AsQueryable<T>()
        {
            return this.GetStoreage<T>()
                       .Values
                       .AsQueryable();
        }

        public T Find<T>(object id)
        {
            T value;
            this.GetStoreage<T>()
                .TryGetValue(id, out value);
            return value;

        }

        public void Remove<T>(T model)
        {
            var storage = this.GetStoreage<T>();

            if (storage.ContainsValue(model))
            {
                var kv = storage.First(s => s.Value.Equals(model));
                storage.Remove(kv.Key);
            }
        }

        public void Update<T>(T model)
        {
            
        }

        private Dictionary<object, T> GetStoreage<T>()
        {
            var type = typeof(T);
            object o;
            if(!this._storage.TryGetValue(type,out o))
            {
                o = new Dictionary<object, T>();
                this._storage.Add(type, o);
            }

            return o as Dictionary<object, T>;
            
        }
    }
}
