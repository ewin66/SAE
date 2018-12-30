using SAE.CommonLibrary.Common;
using SAE.CommonLibrary.Json;
using SAE.CommonLibrary.Log;
using System;

namespace SAE.CommonLibrary.EventStore.Queryable.Handle
{
    public sealed class DefaultAddHandler<Model, TEvent> : DefaultHandler<Model, TEvent> where Model 
                                                         : class, new() where TEvent : IEvent
    {
        private readonly IJsonConvertor _jsonConvertor;
        public DefaultAddHandler(IPersistenceService persistenceService,
                                 IJsonConvertor jsonConvertor,
                                 ILog<DefaultHandler<Model,TEvent>> log) : base(persistenceService, log)
        {
            this._jsonConvertor = jsonConvertor;
        }

        public override void Handle(TEvent @event)
        {
            var model = new Model();
            model.Extend(@event);
            this._log.Info("model:{0}", model);
            this._persistenceService.Add(model);
        }
        
    }
}
