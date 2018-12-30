using SAE.CommonLibrary.Log;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.EventStore.Queryable.Handle
{
    public sealed class DefaultRemoveHandler<Model, TEvent> : DefaultHandler<Model, TEvent> where Model
                                                : class, new() where TEvent : IEvent
    {
        public DefaultRemoveHandler(IPersistenceService persistenceService,
                                    ILog<DefaultHandler<Model, TEvent>> log) : base(persistenceService, log)
        {

        }

        public override void Handle(TEvent @event)
        {
            var model = this._persistenceService.Find<Model>(@event.Id);
            if (model==null)
            {
                this._log.Info($"The id is not found '{@event.Id}' '{typeof(Model)}'");
                throw new NullReferenceException(nameof(model));
            }
            this._persistenceService.Remove(model);
        }
    }
}
