using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Domain.Event
{
    public class ChangeAppNameEvent:Event
    {
        public string Name { get; set; }
    }
}
