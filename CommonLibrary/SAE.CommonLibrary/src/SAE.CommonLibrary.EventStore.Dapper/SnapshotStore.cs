using System;
using System.Threading.Tasks;
using Dapper;
using SAE.CommonLibrary.EventStore.Snapshot;

namespace SAE.CommonLibrary.EventStore.Document.Dapper
{
    public class SnapshotStore:ISnapshotStore
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public SnapshotStore(IDbConnectionFactory dbConnectionFactory)
        {
            this._dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<Snapshot.Snapshot> FindAsync(IIdentity identity, long version)
        {
            Snapshot.Snapshot snapshot;

            using (var conn = this._dbConnectionFactory.Get())
            {
                snapshot = await conn.QueryFirstOrDefaultAsync<Snapshot.Snapshot>($"select data,version,type from {nameof(Snapshot)} where id=@id and version=@version limit 1", new
                {
                    Id = identity.ToString(),
                    Version = version
                }) ?? new Snapshot.Snapshot();
            }

            snapshot.Id = identity.ToString();

            return snapshot;
        }

        public async Task<Snapshot.Snapshot> FindAsync(IIdentity identity)
        {
            Snapshot.Snapshot snapshot;

            using (var conn = this._dbConnectionFactory.Get())
            {
                snapshot =await conn.QueryFirstOrDefaultAsync<Snapshot.Snapshot>($"select data,version,type from {nameof(Snapshot)} where id=@id order by version desc limit 1", new
                {
                    Id = identity.ToString()
                }) ?? new Snapshot.Snapshot();
            }
            snapshot.Id = identity.ToString();
            return snapshot;
        }

        public async Task RemoveAsync(IIdentity identity)
        {
            using (var conn = this._dbConnectionFactory.Get())
            {
                await conn.ExecuteAsync($"delete {nameof(Snapshot)} where id=@id", new { id = identity.ToString() });
            }
        }

        public async Task SaveAsync(Snapshot.Snapshot snapshot)
        {
            using (var conn = this._dbConnectionFactory.Get())
            {
                if (await conn.ExecuteAsync($"insert into {nameof(Snapshot)}(id,type,version,data) values(@id,@type,@version,@data)", snapshot) != 1)
                {
                    throw new Exception("快照添加失败");
                }
            }
        }
    }
}
