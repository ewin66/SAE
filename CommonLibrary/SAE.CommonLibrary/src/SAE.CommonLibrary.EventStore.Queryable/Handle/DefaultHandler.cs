using SAE.CommonLibrary.MQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.EventStore.Queryable.Handle
{
    public abstract class DefaultHandler<Model, TEvent>:IHandler<TEvent> where Model : class, new() where TEvent : IEvent
    {
        protected readonly IPersistenceService _persistenceService;
        protected readonly IAssignmentService _assignmentService;
        public DefaultHandler(IPersistenceService persistenceService,
                              IAssignmentService assignmentService)
        {
            this._persistenceService = persistenceService;
            this._assignmentService = assignmentService;
        }

        public abstract void Handle(TEvent message);
    }
}
