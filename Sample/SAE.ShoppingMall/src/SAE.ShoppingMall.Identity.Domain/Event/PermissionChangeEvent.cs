using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Domain.Event
{
    public class PermissionChangeEvent:Event
    {
        public string Name { get; set; }
        public string Pattern { get; set; }
        public string Group { get; set; }
    }
}
