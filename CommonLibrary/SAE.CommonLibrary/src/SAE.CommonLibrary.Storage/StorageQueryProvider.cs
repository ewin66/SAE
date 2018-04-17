using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SAE.CommonLibrary.Storage
{
    ///// <summary>
    ///// 查询提供
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    //public abstract class StorageQueryProvider : IQueryProvider
    //{
    //    public StorageQueryProvider()
    //    {

    //    }
    //    public IQueryable CreateQuery(Expression expression)
    //    {
    //        return Activator.CreateInstance(
    //               typeof(StorageQueryable<>).MakeGenericType(expression.Type),
    //               new object[] { this, expression }) as IQueryable;
    //    }

    //    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
    //    {
    //        return new StorageQueryable<TElement>(this, expression);
    //    }
    //    public virtual object Execute(Expression expression)
    //    {
    //        return this.ExecuteCore(expression);
    //    }
    //    public virtual TResult Execute<TResult>(Expression expression)
    //    {
    //        return (TResult)this.Execute(expression);
    //    }

    //    protected virtual object ExecuteCore(Expression expression)
    //    {
    //        this.Resolve(expression);
    //        return null;
    //    }

    //    protected  void Resolve(Expression expression)
    //    {
    //        switch (expression.NodeType)
    //        {
    //            case ExpressionType.Decrement:
    //            case ExpressionType.Increment:
    //            case ExpressionType.Negate:
    //            case ExpressionType.NegateChecked:
    //            case ExpressionType.Not:
    //            case ExpressionType.PostDecrementAssign:
    //            case ExpressionType.PostIncrementAssign:
    //            case ExpressionType.PreDecrementAssign:
    //            case ExpressionType.PreIncrementAssign:
    //                this.VisitUnitary(expression);
    //                break;
    //            case ExpressionType.Add:
    //            case ExpressionType.AddAssign:
    //            case ExpressionType.AddAssignChecked:
    //            case ExpressionType.AddChecked:
    //            case ExpressionType.AndAlso:
    //            case ExpressionType.Coalesce:
    //            case ExpressionType.Divide:
    //            case ExpressionType.DivideAssign:
    //            case ExpressionType.Equal:
    //            case ExpressionType.GreaterThan:
    //            case ExpressionType.GreaterThanOrEqual:
    //            case ExpressionType.LessThan:
    //            case ExpressionType.LessThanOrEqual:
    //            case ExpressionType.Modulo:
    //            case ExpressionType.ModuloAssign:
    //            case ExpressionType.Multiply:
    //            case ExpressionType.MultiplyAssign:
    //            case ExpressionType.MultiplyAssignChecked:
    //            case ExpressionType.MultiplyChecked:
    //            case ExpressionType.NotEqual:
    //            case ExpressionType.OrElse:
    //            case ExpressionType.Subtract:
    //            case ExpressionType.SubtractAssign:
    //            case ExpressionType.SubtractAssignChecked:
    //            case ExpressionType.SubtractChecked:
    //                this.VisitBinary(expression);
    //                break;
    //            case ExpressionType.Quote:
    //            case ExpressionType.Conditional:
    //                this.VisitQuote(expression);
    //                break;
    //            case ExpressionType.Call:
    //                this.VisitCall(expression as MethodCallExpression);
    //                break;
    //            case ExpressionType.Lambda:
    //                this.VisitLambda(expression as LambdaExpression);
    //                break;
    //            case ExpressionType.MemberAccess:
    //                this.VisitMemberAccess(expression as MemberExpression);
    //                break;
    //            case ExpressionType.Constant:
    //                this.VisitConstant(expression as ConstantExpression);
    //                break;
    //        }
            
    //    }

    //    protected abstract void VisitConstant(ConstantExpression constantExpression);

    //    protected abstract void VisitMemberAccess(MemberExpression memberExpression);

    //    protected virtual void VisitLambda(LambdaExpression expression)
    //    {
    //        this.Resolve(expression.Body);
    //    }
    //    /// <summary>
    //    /// 访问执行表达式
    //    /// </summary>
    //    /// <param name="expression"></param>
    //    protected virtual void VisitCall(MethodCallExpression expression)
    //    {
    //        foreach(var arg in expression.Arguments)
    //        {
    //            this.Resolve(arg);
    //        }
    //    }
    //    protected virtual void VisitQuote(Expression expression)
    //    {
    //        this.Resolve((expression as UnaryExpression).Operand);
    //    }
    //    /// <summary>
    //    /// 访问二元表达式
    //    /// </summary>
    //    /// <param name="expression"></param>
    //    protected abstract void VisitBinary(Expression expression);
    //    /// <summary>
    //    /// 访问一元表达式
    //    /// </summary>
    //    /// <param name="expression"></param>
    //    protected abstract void VisitUnitary(Expression expression);
    //    /// <summary>
    //    /// 访问参数表达式
    //    /// </summary>
    //    /// <param name="expression"></param>
    //    protected abstract void VisitParameter(Expression expression);

    //}
}
