using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Domain.Event
{
    public class RemoveRoleEvent : Event
    {
        public string Id { get; set; }
    }
}
