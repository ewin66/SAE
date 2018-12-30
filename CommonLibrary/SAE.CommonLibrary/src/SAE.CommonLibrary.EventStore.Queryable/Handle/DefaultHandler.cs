using SAE.CommonLibrary.Log;
using SAE.CommonLibrary.MQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.EventStore.Queryable.Handle
{
    public abstract class DefaultHandler<Model, TEvent>:IHandler<TEvent> where Model : class, new() where TEvent : IEvent
    {
        protected readonly IPersistenceService _persistenceService;
        protected readonly ILog _log;
        public DefaultHandler(IPersistenceService persistenceService,
                              ILog log)
        {
            this._persistenceService = persistenceService;
            this._log = log;
        }

        public abstract void Handle(TEvent message);
    }
}
