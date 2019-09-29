using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using exp = System.Linq.Expressions;

namespace SAE.CommonLibrary.Common.Specification.Expression
{

    public static class SpecificationExtension
    {
        /// <summary>
        /// 和
        /// </summary>
        /// <typeparam name="TSpec"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static ISpecification<TSpec> And<TSpec>(this ISpecification<TSpec> left, ISpecification<TSpec> right)
        {
            return new AndSpecification<TSpec>(left, right);
        }

        /// <summary>
        /// 或
        /// </summary>
        /// <typeparam name="TSpec"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static ISpecification<TSpec> Or<TSpec>(this ISpecification<TSpec> left,ISpecification<TSpec> right)
        {
            return new OrSpecification<TSpec>(left, right);
        }

        /// <summary>
        /// 不等于
        /// </summary>
        /// <typeparam name="TSpec"></typeparam>
        /// <param name="specification"></param>
        /// <returns></returns>
        public static ISpecification<TSpec> Not<TSpec>(this ISpecification<TSpec> specification)
        {
            return new NotSpecification<TSpec>(specification);
        }

    }

    /// <summary>
    /// 规约表达式
    /// </summary>
    public static class ExpressionSpecificationExtension
    {
        /// <summary>
        /// 和
        /// </summary>
        /// <typeparam name="TSpec"></typeparam>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <returns></returns>
        public static Expression<Func<TSpec,bool>> And<TSpec>(this Expression<Func<TSpec, bool>> one, Expression<Func<TSpec, bool>> two)
        {
            if (one == null) return two;
            if (two == null) return one;
            var parameterExpression = exp.Expression.Parameter(typeof(TSpec));
            var parameterReplacer = new ParameterReplacer(parameterExpression);
            var left = parameterReplacer.Replace(one);
            var right = parameterReplacer.Replace(two);
            var body = exp.Expression.And(left, right);
            return exp.Expression.Lambda<Func<TSpec, bool>>(body,parameterExpression);
        }

        /// <summary>
        /// 或
        /// </summary>
        /// <typeparam name="TSpec"></typeparam>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <returns></returns>
        public static Expression<Func<TSpec,bool>> Or<TSpec>(this Expression<Func<TSpec,bool>> one,Expression<Func<TSpec,bool>> two)
        {
            if (one == null) return two;
            if (two == null) return one;
            var parameterExpression = exp.Expression.Parameter(typeof(TSpec));
            var parameterReplacer = new ParameterReplacer(parameterExpression);
            var left = parameterReplacer.Replace(one);
            var right = parameterReplacer.Replace(two);
            var body = exp.Expression.Or(left, right);
            return exp.Expression.Lambda<Func<TSpec, bool>>(body, parameterExpression);
        }

        /// <summary>
        /// 不等于
        /// </summary>
        /// <typeparam name="TSpec"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Expression<Func<TSpec, bool>> Not<TSpec>(this Expression<Func<TSpec,bool>> expression)
        {
            var parameterExpression = expression.Parameters[0];

            var body = exp.Expression.Not(expression.Body);

            return exp.Expression.Lambda<Func<TSpec, bool>>(body, parameterExpression);
        }

    }

    internal class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _parameterExpression;
        public ParameterReplacer(ParameterExpression parameterExpression)
        {
            this._parameterExpression = parameterExpression;
        }


        public exp.Expression Replace(exp.Expression expression)
        {
            return this.Visit(expression);
        }


        protected override exp.Expression VisitParameter(ParameterExpression node)
        {
            return this._parameterExpression;
        }
    }
}
