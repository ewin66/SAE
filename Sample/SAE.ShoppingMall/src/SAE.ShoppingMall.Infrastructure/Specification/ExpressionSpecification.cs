using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SAE.ShoppingMall.Infrastructure.Specification.Expression
{

    public class OrSpecification<TSpec> : ISpecification<TSpec>
    {
        public OrSpecification(ISpecification<TSpec> left, ISpecification<TSpec> right)
        {
            this.Expression = left.Expression.Or(right.Expression);
        }

        public Expression<Func<TSpec, bool>> Expression { get; }
    }

    public class AndSpecification<TSpec> : ISpecification<TSpec>
    {
        public AndSpecification(ISpecification<TSpec> left,ISpecification<TSpec> right)
        {
            this.Expression = left.Expression.And(right.Expression);
        }

        public Expression<Func<TSpec, bool>> Expression
        {
            get;
        }
    }

    public class NotSpecification<TSpec> : ISpecification<TSpec>
    {
        public NotSpecification(ISpecification<TSpec>  specification)
        {
            this.Expression = specification.Expression.Not();
        }

        public Expression<Func<TSpec, bool>> Expression { get; }
    }
}
