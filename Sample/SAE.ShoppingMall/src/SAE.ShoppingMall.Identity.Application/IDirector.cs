using SAE.ShoppingMall.Identity.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Application
{
    public interface IDirector
    {
        void Build<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : IAggregateRoot;
    }
    public interface IDirector<TAggregateRoot>where TAggregateRoot: IAggregateRoot
    {
        void Build(TAggregateRoot aggregateRoot);
    }
}
