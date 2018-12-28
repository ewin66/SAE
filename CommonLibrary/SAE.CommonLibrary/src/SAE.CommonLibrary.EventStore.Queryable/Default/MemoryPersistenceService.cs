using System.Collections.Concurrent;

namespace SAE.CommonLibrary.EventStore.Queryable.Default
{
    public class MemoryPersistenceService : IPersistenceService
    {
        private readonly ConcurrentDictionary<string, object> _store;
        public MemoryPersistenceService()
        {
            _store = new ConcurrentDictionary<string, object>();
        }

        public void Add<T>(T t) where T : class
        {
            _store[this.GetKey(t)] = t;
        }

        public T Find<T>(string id) where T : class
        {
            object obj;
            _store.TryGetValue(id, out obj);
            return (T)obj;
        }

        public void Remove<T>(T t) where T : class
        {
            dynamic @dynamic = t;
            object obj;
            _store.TryRemove(this.GetKey(t), out obj);
        }

        public void Update<T>(T t) where T : class
        {
            _store[this.GetKey(t)] = t;
        }

        private string GetKey(object o)
        {
            dynamic @dynamic=o;
            return dynamic.Id.ToString();
        }
    }
}
