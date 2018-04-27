using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Domain.Event
{
    public class ChangeAppStatusEvent:Event
    {
        public Status Status { get; set; }
    }
}
