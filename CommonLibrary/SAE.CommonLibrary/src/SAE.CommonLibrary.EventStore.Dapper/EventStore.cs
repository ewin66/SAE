using System;
using System.Threading.Tasks;
using Dapper;
namespace SAE.CommonLibrary.EventStore.Document.Dapper
{
    public class EventStore : IEventStore
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public EventStore(IDbConnectionFactory dbConnectionFactory)
        {
            this._dbConnectionFactory = dbConnectionFactory;
        }
        public async Task AppendAsync(EventStream eventStream)
        {
            using (var conn= this._dbConnectionFactory.Get())
            {
                if (await conn.ExecuteAsync("insert into EventStream(id,timestamp,version,data) values(@id,@timestamp,@version,@data)", new
                {
                    Id = eventStream.Identity.ToString(),
                    Timestamp = eventStream.TimeStamp,
                    eventStream.Version,
                    Data = eventStream.ToString()
                }) != 1)
                {
                    throw new Exception("事件流添加失败");
                }
            }
        }

        public async Task<EventStream> LoadEventStreamAsync(IIdentity identity, long skipEvents, int maxCount)
        {
            var eventStream = new EventStream(identity, 0, events: null, timeStamp: DateTimeOffset.MinValue);
            using (var conn = this._dbConnectionFactory.Get())
            {
                using (var reader =await conn.ExecuteReaderAsync($"select * from EventStream where id=@id and version > @skipVersion limit {maxCount}",
                                                                 new
                                                                 {
                                                                     Id = identity.ToString(),
                                                                     SkipVersion = skipEvents
                                                                 }))
                {
                    while (reader.Read())
                    {
                        eventStream.Append(new EventStream(identity,
                                           reader.GetInt32(reader.GetOrdinal("version")),
                                           reader.GetString(reader.GetOrdinal("data")),
                                           DateTimeOffset.Parse(reader.GetString(reader.GetOrdinal("timestamp")))));
                    }
                }
            }
            return eventStream;
        }

        public async Task<long> GetVersionAsync(IIdentity identity)
        {
            using (var conn = this._dbConnectionFactory.Get())
            {
                return await conn.ExecuteScalarAsync<int>("select version from EventStream where id=@id order by version desc limit 1");
            }
        }
    }
}
