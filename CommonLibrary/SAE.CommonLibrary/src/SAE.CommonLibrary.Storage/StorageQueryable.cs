namespace SAE.CommonLibrary.Storage
{
    ///// <summary>
    ///// 查询
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    //public class StorageQueryable<T> : IQueryable<T>
    //{
    //    public StorageQueryable(IQueryProvider provider)
    //    {
    //        Assert.IsNotNull(provider, $"{nameof(provider)}尚未提供", StateCode.InvalidParameter);
    //        this.Provider = provider;
    //        this.Expression = Expression.Constant(this);
    //    }

    //    public StorageQueryable(IQueryProvider provider,Expression expression)
    //    {
    //        Assert.IsNotNull(provider,$"{nameof(provider)}尚未提供", StateCode.InvalidParameter);
    //        Assert.IsNotNull(expression,$"{nameof(expression)}尚未提供",StateCode.InvalidParameter);
    //        this.ElementType = typeof(T);
    //        this.Expression = expression;
    //        this.Provider = provider;
    //    }
    //    public Type ElementType
    //    {
    //        get;
    //    }

    //    public Expression Expression
    //    {
    //        get;
    //    }

    //    public IQueryProvider Provider
    //    {
    //        get;
    //    }

    //    public IEnumerator<T> GetEnumerator()
    //    {
    //        return ((IEnumerable<T>)this.Provider.Execute(this.Expression)).GetEnumerator();
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return ((IEnumerable)this.Provider.Execute(this.Expression)).GetEnumerator();
    //    }
    //}
}
