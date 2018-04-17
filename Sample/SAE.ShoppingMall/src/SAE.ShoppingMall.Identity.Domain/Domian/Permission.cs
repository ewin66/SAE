using System;
using SAE.CommonLibrary.EventStore;

namespace SAE.ShoppingMall.Identity.Domain
{
    public class Permission: AggregateRoot
    {
        public override IIdentity Identity => throw new NotImplementedException();
    }
}
