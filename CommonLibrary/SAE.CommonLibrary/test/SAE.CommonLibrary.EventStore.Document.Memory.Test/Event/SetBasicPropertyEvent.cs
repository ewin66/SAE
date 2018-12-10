using SAE.CommonLibrary.EventStore.Queryable.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.EventStore.Document.Memory.Test.Event
{
    [Model("SAE.CommonLibrary.EventStore.Document.Memory.Test.Domain.User,SAE.CommonLibrary.EventStore.Document.Memory.Test",HandlerEnum.Update)]
    public class SetBasicPropertyEvent:IEvent
    {
        public SetBasicPropertyEvent()
        {

        }

        public string Name { get; set; }
        public int Sex { get; set; }

        public string Id { get; set; }

        public long Version { get; set; }
    }
}
