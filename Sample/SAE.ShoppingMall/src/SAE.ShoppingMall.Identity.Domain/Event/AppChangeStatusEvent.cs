using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Domain.Event
{
    public class AppChangeStatusEvent:Event
    {
        public Status Status { get; set; }
    }
}
