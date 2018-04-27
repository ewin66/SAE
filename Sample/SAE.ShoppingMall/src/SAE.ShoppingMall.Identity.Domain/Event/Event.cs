using System;
using System.Collections.Generic;
using System.Text;
using SAE.CommonLibrary.EventStore;

namespace SAE.ShoppingMall.Identity.Domain.Event
{
    public class Event : IEvent
    {
        string IEvent.Id { get; set; }
        long IEvent.Version { get; set; }
    }
}
