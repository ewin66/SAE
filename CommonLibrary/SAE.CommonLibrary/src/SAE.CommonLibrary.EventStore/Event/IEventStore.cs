using System.Threading.Tasks;

namespace SAE.CommonLibrary.EventStore
{
    /// <summary>
    /// 事件存储
    /// </summary>
    public interface IEventStore
    {
        /// <summary>
        /// 获得事件流
        /// </summary>
        /// <param name="identity">通过标识获得事件流</param>
        /// <param name="skipEvents">跳过的事件</param>
        /// <param name="maxCount"></param>
        /// <returns></returns>
        Task<EventStream> LoadEventStreamAsync(IIdentity identity,long skipEvents,int maxCount);

        /// <summary>
        /// 附加事件
        /// </summary>
        /// <param name="eventStream">要附加的事件流</param>
        Task AppendAsync(EventStream eventStream);
        /// <summary>
        /// 使用<paramref name="identity"/>返回当前版本号
        /// </summary>
        /// <param name="identity">事件流标识</param>
        /// <returns></returns>
        Task<long> GetVersionAsync(IIdentity identity);

        /// <summary>
        /// 将事件彻底从事件存储移除
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        Task RemoveAsync(IIdentity identity);
    }
}
