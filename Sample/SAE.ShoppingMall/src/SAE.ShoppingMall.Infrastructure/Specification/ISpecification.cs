using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SAE.ShoppingMall.Infrastructure.Specification
{
    public interface ISpecification<TSpec>
    {
        Expression<Func<TSpec,bool>> Expression { get; }
    }
}
