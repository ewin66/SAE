using System;
using System.Collections.Generic;

namespace SAE.CommonLibrary.Log
{
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum Level
    {
        /// <summary>
        /// 跟踪
        /// </summary>
        Trace,
        /// <summary>
        /// 调试
        /// </summary>
        Debug,

        /// <summary>
        /// 信息
        /// </summary>
        Info,

        /// <summary>
        /// 警告
        /// </summary>
        Warn,

        /// <summary>
        /// 错误
        /// </summary>
        Error,

        /// <summary>
        /// 致命错误
        /// </summary>
        Fatal
        
    }

    /// <summary>
    /// 日志组件（以下API均为异步实现，不支持线程堵塞）
    /// </summary>
    public interface ILog
    {

        /// <summary>
        /// 该日志级别是否启用
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        bool IsEnabled(Level level);

        /// <summary>
        /// 写入日志并指定写入类型，和异常触发者
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="level">日志类型</param>
        /// <param name="exception">异常</param>
        ILog Write(string message, Level level, Exception exception);
    }

    /// <summary>
    /// 日志组件泛型接口
    /// </summary>
    /// <typeparam name="TCategory">日志记录器的类型</typeparam>
    public interface ILog< out TCategory> :ILog
    {

    }
}