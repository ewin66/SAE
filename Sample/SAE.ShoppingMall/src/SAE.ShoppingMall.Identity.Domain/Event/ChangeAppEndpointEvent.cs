using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Domain.Event
{
    public class ChangeAppEndpointEvent:Event
    {
        public string Signin { get; set; }
        public string Signout { get; set; }
    }
}
