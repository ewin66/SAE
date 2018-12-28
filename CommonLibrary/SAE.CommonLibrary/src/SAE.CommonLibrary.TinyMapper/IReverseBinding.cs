using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SAE.CommonLibrary.ObjectMapper
{
    internal class ReverseBinding<TTarget, TSource> : IReverseBinding<TTarget, TSource>
    {
        public ReverseBinding(IEnumerable<KeyValuePair<Expression<Func<TSource, object>>, Expression<Func<TTarget, object>>>> bindExpression)
        {
            this.BindExpression = bindExpression;
        }
        public IEnumerable<KeyValuePair<Expression<Func<TSource, object>>, Expression<Func<TTarget, object>>>> BindExpression { get; }
    }
    public interface IReverseBinding<TTarget,TSource>
    {
        IEnumerable<KeyValuePair<Expression<Func<TSource, object>>, Expression<Func<TTarget, object>>>> BindExpression { get; }
    }
}
