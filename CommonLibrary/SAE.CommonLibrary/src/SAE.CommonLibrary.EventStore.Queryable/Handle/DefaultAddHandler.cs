using SAE.CommonLibrary.MQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.EventStore.Queryable.Handle
{
    public sealed class DefaultAddHandler<Model, TEvent> : DefaultHandler<Model, TEvent> where Model 
                                             : class, new() where TEvent : IEvent
    {

        public DefaultAddHandler(IPersistenceService persistenceService, IAssignmentService assignmentService) : base(persistenceService, assignmentService)
        {

        }

        public override void Handle(TEvent @event)
        {
            var model = new Model();
            this._assignmentService.Transfer(@event,model);
            this._persistenceService.Add(model);
        }
        
    }
}
