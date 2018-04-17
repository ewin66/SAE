using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SAE.CommonLibrary.EventStore.Document
{
    /// <summary>
    /// 文档事件
    /// </summary>
    public interface IDocumentEvent
    {
        /// <summary>
        /// 附加事件
        /// </summary>
        /// <param name="document">附加后的文档对象</param>
        /// <param name="events">附加对象</param>
        Task AppendAsync(IDocument document, IEnumerable<IEvent> events);
    }
}
