using System;

namespace SAE.CommonLibrary.Log
{
    /// <summary>
    /// 日志组件（以下API均为异步实现，不支持线程堵塞）
    /// </summary>
    public interface ILog
    {
        ///// <summary>
        ///// 写入以Info类型写入日志
        ///// </summary>
        ///// <param name="message">日志消息</param>
        //void Write(string message);

        ///// <summary>
        ///// 写入日志并指定写入类型
        ///// </summary>
        ///// <param name="message">日志消息</param>
        ///// <param name="messageType">日志类型</param>
        //void Write(string message, MessageType messageType);

        /// <summary>
        /// 写入日志并指定写入类型，和异常触发者
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="messageType">日志类型</param>
        /// <param name="ex">异常</param>
        void Write(string message, MessageType messageType, Exception ex);
    }

    /// <summary>
    /// 日志组件泛型接口
    /// </summary>
    /// <typeparam name="TCategory">日志记录器的类型</typeparam>
    public interface ILog< out TCategory> :ILog
    {

    }
}