using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Domain.Event
{
    public class CreatePermissionEvent:Event
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Pattern { get; set; }
    }
}
