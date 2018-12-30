using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Domain.Event
{
    public class AppRemoveEvent:Event
    {
        public string Id { get; set; }
    }
}
