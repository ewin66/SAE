using System;
using SAE.CommonLibrary.EventStore;

namespace SAE.ShoppingMall.Identity.Domain
{
    public class Role: AggregateRoot
    {
        public override IIdentity Identity => throw new NotImplementedException();
    }
}
