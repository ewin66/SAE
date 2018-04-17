using SAE.CommonLibrary.EventStore.Serialize;
using SAE.CommonLibrary.EventStore.Snapshot;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SAE.CommonLibrary.EventStore.Document
{
    public class DefaultDocumentStore : IDocumentStore
    {
        private readonly ISnapshotStore _snapshot;
        private readonly ISerializer _serializer=SerializerProvider.Current;
        private readonly IEventStore _eventStore;
        private readonly IDocumentEvent _documentEvent;
        
        public DefaultDocumentStore(ISnapshotStore snapshot,IEventStore eventStore,IDocumentEvent documentEvent)
        {
            this._snapshot = snapshot;
            this._eventStore = eventStore;
            this._documentEvent = documentEvent;
        }

        public virtual async Task<TDocument> FindAsync<TDocument>(IIdentity identity)where TDocument:IDocument,new()
        {
            //获取最新快照
            var snapshot = await this._snapshot.FindAsync(identity) ?? new Snapshot.Snapshot(identity);
            //加载事件流
            var eventStream = await this._eventStore.LoadEventStreamAsync(identity, snapshot.Version, int.MaxValue);
            TDocument document;
            if (snapshot.Version <= 0)
            {
                document = new TDocument();
            }
            else
            {
                document = this._serializer.Deserialize<TDocument>(snapshot.Data);
            }
            //序列化文档
            
            //重放事件
            foreach(IEvent @event in eventStream)
            {
                document.Mutate(@event);
            }
            document.Version = eventStream.Version <= 0 ? snapshot.Version : eventStream.Version;
            return document;
        }

        public virtual async Task<TDocument> FindAsync<TDocument>(IIdentity identity, long version) where TDocument : IDocument,new()
        {
            //获取快照
            var snapshot = await this.FindSnapshotAsync(identity, version);
            //加载事件流
            var eventStream =await this._eventStore.LoadEventStreamAsync(identity, snapshot.Version, (int)(version-snapshot.Version));
            //序列化文档
            var document = this._serializer.Deserialize<TDocument>(snapshot.Data);
            //重放事件
            foreach (IEvent @event in eventStream)
            {
                document.Mutate(@event);
            }
            document.Version = eventStream.Version <= 0 ? snapshot.Version : eventStream.Version;
            return document;
        }

        /// <summary>
        /// 查找快照
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        protected virtual async Task<Snapshot.Snapshot> FindSnapshotAsync(IIdentity identity,long version)
        {

            var snapshotInterval = Config.SnapshotInterval.Invoke();
            if (version <= snapshotInterval)
            {
                return new Snapshot.Snapshot();
            }

            var snapshotVersion = version - version % snapshotInterval;

            var snapshot =await this._snapshot.FindAsync(identity, snapshotVersion);

            if (snapshot == null)
            {
                return await this.FindSnapshotAsync(identity, version - snapshotInterval);
            }

            return snapshot;
        }

        public virtual async Task SaveAsync(IDocument document)
        {
            var identity = document.Identity;
            var currentVersion = await this._eventStore.GetVersionAsync(identity);

            if (currentVersion > document.Version)
            {
                throw new Exception("当前版本过低");
            }
            //将当前版本号+1以抱持循序性
            var version = currentVersion + 1;
            //创建事件流
            var eventStream = new EventStream(identity, version, document.ChangeEvents);
            //累加事件流
            await this._eventStore.AppendAsync(eventStream);

            await this._documentEvent.AppendAsync(document, eventStream);
            //如果版本号满足快照要求就将对象存储到快照中
            if (version % Config.SnapshotInterval.Invoke() != 0)
            {
                //不满足快照要求
                return;
            }

            //从快照存储获取对应快照
            var snapshot=await this._snapshot.FindAsync(identity);
            //反序列化文档
            document = this._serializer.Deserialize(snapshot.Data, Type.GetType(snapshot.Type)) as IDocument;
            //重放事件
            foreach (IEvent @event in eventStream)
            {
                document.Mutate(@event);
            }
            //
            await this._snapshot.SaveAsync(new Snapshot.Snapshot(identity, document, version));
            
        }
    }
}
