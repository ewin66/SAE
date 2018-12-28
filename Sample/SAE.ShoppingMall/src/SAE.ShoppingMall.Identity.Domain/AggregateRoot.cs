using System.Collections.Generic;
using SAE.CommonLibrary.Common;
using SAE.CommonLibrary.EventStore;
using SAE.CommonLibrary.EventStore.Document;

namespace SAE.ShoppingMall.Identity.Domain
{
    public abstract class AggregateRoot : IAggregateRoot, IDocument
    {
        protected readonly IList<IEvent> _changeEvents=new List<IEvent>();

        protected virtual void Apply(IEvent @event)
        {
            this._changeEvents.Add(@event);

            ((IDocument)this).Mutate(@event);
        }
        IEnumerable<IEvent> IDocument.ChangeEvents => this._changeEvents;

        long IDocument.Version { get; set; }
        public abstract IIdentity Identity { get; }

        void IDocument.Mutate(IEvent @event)
        {
            this.Extend(@event);
            //dynamic self = this;
            //self.When((dynamic)@event);
        }
    }
}
