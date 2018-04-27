using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Domain.Event
{
    public class ChangeAppSecretEvent:Event
    {
        public string Secret { get; set; }
    }
}
