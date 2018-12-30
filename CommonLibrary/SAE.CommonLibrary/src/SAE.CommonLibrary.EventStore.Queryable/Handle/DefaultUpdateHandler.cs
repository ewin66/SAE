using SAE.CommonLibrary.Common;
using SAE.CommonLibrary.Json;
using SAE.CommonLibrary.Log;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.EventStore.Queryable.Handle
{
    public sealed class DefaultUpdateHandler<Model, TEvent> : DefaultHandler<Model, TEvent> where Model
                                                : class, new() where TEvent : IEvent
    {
        public DefaultUpdateHandler(IPersistenceService persistenceService,
                                    ILog<DefaultHandler<Model, TEvent>> log) : base(persistenceService, log)
        {
        }

        public override void Handle(TEvent @event)
        {
            var model = this._persistenceService.Find<Model>(@event.Id);
            if (model == null)
            {
                this._log.Info($"The id is not found '{@event.Id}' '{typeof(Model)}'");
                throw new NullReferenceException(nameof(model));
            }

            this._log.Info("model:{1}", model);

            model.Extend(@event);

            this._persistenceService.Update(model);
        }
    }
}
