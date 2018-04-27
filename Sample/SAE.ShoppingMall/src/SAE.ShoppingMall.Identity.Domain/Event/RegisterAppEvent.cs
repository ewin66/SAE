using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Domain.Event
{
    public class RegisterAppEvent:Event
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public string Name { get; set; }
        public string Signin { get; set; }
        public string Signout { get; set; }
        public DateTime CreateTime { get; set; }
        public Status Status { get; set; }
    }
}
