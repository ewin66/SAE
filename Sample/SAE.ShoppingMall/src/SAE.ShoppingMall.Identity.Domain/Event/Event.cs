using System;
using System.Collections.Generic;
using System.Text;
using SAE.CommonLibrary.EventStore;

namespace SAE.ShoppingMall.Identity.Domain.Event
{
    public class Event : IEvent
    {
        public string Id { get; set; }
        public long Version { get; set; }
    }
}
