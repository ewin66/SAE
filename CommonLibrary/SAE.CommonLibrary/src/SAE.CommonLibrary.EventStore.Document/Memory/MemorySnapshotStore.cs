using SAE.CommonLibrary.EventStore.Snapshot;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.CommonLibrary.EventStore.Document.Memory
{
    /// <summary>
    /// 基于内存的快照存储
    /// </summary>
    public class MemorySnapshotStore : ISnapshotStore
    {
        private readonly List<Snapshot.Snapshot> _store;
        public MemorySnapshotStore()
        {
            _store = new List<Snapshot.Snapshot>();
        }
        public Task<Snapshot.Snapshot> FindAsync(IIdentity identity, long version)
        {
            return Task.FromResult(_store.FirstOrDefault(s => s.Id == identity.ToString() && s.Version == version));
        }

        public Task<Snapshot.Snapshot> FindAsync(IIdentity identity)
        {
            return Task.FromResult(_store.FirstOrDefault(s => s.Id == identity.ToString()));
        }

        public Task RemoveAsync(IIdentity identity)
        {
            this._store.RemoveAll(s => s.Id == identity.ToString());
            return Task.CompletedTask;
        }

        public Task SaveAsync(Snapshot.Snapshot snapshot)
        {
            this._store.Add(snapshot);
            return Task.CompletedTask;
        }
    }
}
