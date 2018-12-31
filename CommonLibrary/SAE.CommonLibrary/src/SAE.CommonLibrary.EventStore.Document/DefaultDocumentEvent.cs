using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.CommonLibrary.EventStore.Document
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultDocumentEvent : IDocumentEvent
    {
        /// <summary>
        /// 附加事件
        /// </summary>
        public event Func<IDocument, IEnumerable<IEvent>, Task> OnAppend;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <param name="events"></param>
        public async Task AppendAsync(IDocument document, IEnumerable<IEvent> events)
        {
            await this.OnAppend?.Invoke(document, events);
        }
    }
}
