using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.EventStore.Queryable.Handle
{
    public sealed class DefaultRemoveHandler<Model, TEvent> : DefaultHandler<Model, TEvent> where Model
                                                : class, new() where TEvent : IEvent
    {
        public DefaultRemoveHandler(IPersistenceService persistenceService, IAssignmentService assignmentService) : base(persistenceService, assignmentService)
        {
        }

        public override void Handle(TEvent @event)
        {
            var model = this._persistenceService.GetById<Model>(@event.Id);
            if (model != null)
                this._persistenceService.Remove(model);
        }
    }
}
