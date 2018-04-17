using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.MQ
{
    /// <summary>
    /// 处理接口
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public interface IHandler<TMessage>: IHandler
    {
        /// <summary>
        /// 执行处理程序
        /// </summary>
        /// <param name="message"></param>
        void Handle(TMessage message);
    }

    /// <summary>
    /// 处理程序标记接口
    /// </summary>
    public interface IHandler
    {

    }


}
