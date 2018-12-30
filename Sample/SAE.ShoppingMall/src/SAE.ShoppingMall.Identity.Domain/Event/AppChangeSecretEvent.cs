using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Domain.Event
{
    public class AppChangeSecretEvent:Event
    {
        public string Secret { get; set; }
    }
}
