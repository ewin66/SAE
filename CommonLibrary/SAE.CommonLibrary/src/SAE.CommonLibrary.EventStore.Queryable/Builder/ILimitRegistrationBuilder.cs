using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.EventStore.Queryable.Builder
{

    /// <summary>
    /// 受限的构建者
    /// </summary>
    public interface ILimitRegistrationBuilder
    {
        /// <summary>
        /// 构建者
        /// </summary>
        IRegistrationBuilder RegistrationBuilder { get; }
        /// <summary>
        /// 模型类型
        /// </summary>
        Type ModelType { get; }
    }

    /// <summary>
    /// <seealso cref="ILimitRegistrationBuilder"/>泛型的实现
    /// </summary>
    /// <typeparam name="TModel">模型类型</typeparam>
    public interface ILimitRegistrationBuilder<TModel>: ILimitRegistrationBuilder where TModel : class
    {
        
    }
}
