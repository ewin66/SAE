using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Domain.Event
{
    public class ChangePermissionEvent:Event
    {
        public string Name { get; set; }
        public string Pattern { get; set; }
    }
}
