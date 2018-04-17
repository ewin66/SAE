using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SAE.CommonLibrary.EventStore;
using SAE.CommonLibrary.EventStore.Document;
using SAE.CommonLibrary.MQ;

namespace SAE.ShoppingMall.DocumentService
{
    public class DocumentPublish : IDocumentEvent
    {
        private readonly IMQ _mq;
        public DocumentPublish(IMQ mq)
        {
            this._mq = mq;
        }
        public async Task AppendAsync(IDocument document, IEnumerable<IEvent> events)
        {
            foreach(var @event in events)
            {
                await this._mq.PublishAsync(@event);
            }
        }
    }
}
