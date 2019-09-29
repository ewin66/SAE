using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SAE.CommonLibrary.Common.Specification.Expression
{
    /// <summary>
    /// Or规约
    /// </summary>
    /// <typeparam name="TSpec"></typeparam>
    public class OrSpecification<TSpec> : ISpecification<TSpec>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public OrSpecification(ISpecification<TSpec> left, ISpecification<TSpec> right)
        {
            this.Expression = left.Expression.Or(right.Expression);
        }
        /// <summary>
        /// 
        /// </summary>
        public Expression<Func<TSpec, bool>> Expression { get; }
    }
    /// <summary>
    /// and 规约
    /// </summary>
    /// <typeparam name="TSpec"></typeparam>
    public class AndSpecification<TSpec> : ISpecification<TSpec>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public AndSpecification(ISpecification<TSpec> left,ISpecification<TSpec> right)
        {
            this.Expression = left.Expression.And(right.Expression);
        }
        /// <summary>
        /// 
        /// </summary>
        public Expression<Func<TSpec, bool>> Expression
        {
            get;
        }
    }
    /// <summary>
    /// not 规约
    /// </summary>
    /// <typeparam name="TSpec"></typeparam>
    public class NotSpecification<TSpec> : ISpecification<TSpec>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="specification"></param>
        public NotSpecification(ISpecification<TSpec>  specification)
        {
            this.Expression = specification.Expression.Not();
        }
        /// <summary>
        /// 
        /// </summary>
        public Expression<Func<TSpec, bool>> Expression { get; }
    }
}
