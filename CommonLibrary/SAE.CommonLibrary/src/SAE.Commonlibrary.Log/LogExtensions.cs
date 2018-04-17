using System;

namespace SAE.CommonLibrary.Log
{
    /// <summary>
    /// 日志扩展
    /// </summary>
    public static class LogExtensions
    {

        #region Debug
        /// <summary>
        /// <see cref="MessageType.Debug"/>级别日志
        /// </summary>
        /// <param name="log">日志记录器</param>
        /// <param name="message">日志消息</param>
        /// <param name="ex">异常</param>
        /// <returns>日志记录器</returns>
        public static ILog Debug(this ILog log, string message, Exception ex = null)
        {

            log.Write(message, MessageType.Debug, ex);

            return log;
        }

        /// <summary>
        /// 记录<see cref="MessageType.Debug"/>别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="provider">字符串格式化提供程序</param>
        /// <param name="format">格式化的字符串</param>
        /// <param name="args">参数集合</param>
        public static ILog DebugFormat(this ILog log, IFormatProvider provider, string format, params object[] args)
        {
            log.Debug(string.Format(provider, format, args));
            return log;
        }
        /// <summary>
        /// 记录<see cref="MessageType.Debug"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="format">格式化的字符串</param>
        /// <param name="args">参数集合</param>
        public static ILog DebugFormat(this ILog log, string format, params object[] args)
        {
            log.Debug(string.Format(format, args));
            return log;
        }
        #endregion

        #region Error

        /// <summary>
        /// <see cref="MessageType.Error"/>级别日志
        /// </summary>
        /// <param name="log">日志记录器</param>
        /// <param name="ex">异常</param>
        /// <returns>日志记录器</returns>
        public static ILog Error(this ILog log, Exception ex)
        {
            log.Error(ex.Message, ex);
            return log;
        }

        /// <summary>
        /// <see cref="MessageType.Error"/>级别日志
        /// </summary>
        /// <param name="log">日志记录器</param>
        /// <param name="message">日志消息</param>
        /// <param name="ex">异常</param>
        /// <returns>日志记录器</returns>
        public static ILog Error(this ILog log, string message, Exception ex = null)
        {

            log.Write(message, MessageType.Error, ex);

            return log;
        }


        /// <summary>
        /// 记录<see cref="MessageType.Error"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="provider">字符串格式化提供程序</param>
        /// <param name="format">格式化的字符串</param>
        /// <param name="args">参数集合</param>
        public static ILog ErrorFormat(this ILog log, IFormatProvider provider, string format, params object[] args)
        {
            log.Error(string.Format(provider, format, args));
            return log;
        }
        /// <summary>
        /// 记录<see cref="MessageType.Error"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="format">格式化的字符串</param>
        /// <param name="args">参数集合</param>
        public static ILog ErrorFormat(this ILog log, string format, params object[] args)
        {
            log.Error(string.Format(format, args));
            return log;
        } 
        #endregion

        #region Fatal
        /// <summary>
        /// <see cref="MessageType.Fatal"/>级别
        /// </summary>
        /// <param name="log">日志记录器</param>
        /// <param name="message">日志消息</param>
        /// <param name="ex">异常</param>
        /// <returns>日志记录器</returns>
        public static ILog Fatal(this ILog log, string message, Exception ex = null)
        {

            log.Write(message, MessageType.Fatal, ex);

            return log;
        }


        /// <summary>
        /// 记录<see cref="MessageType.Fatal"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="format">格式化的字符串</param>
        /// <param name="args">参数集合</param>
        public static ILog FatalFormat(this ILog log, string format, params object[] args)
        {
            log.Fatal(string.Format(format, args));
            return log;
        }
        /// <summary>
        /// 记录<see cref="MessageType.Fatal"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="provider">字符串格式化提供程序</param>
        /// <param name="format">格式化的字符串</param>
        /// <param name="args">参数集合</param>
        public static ILog FatalFormat(this ILog log, IFormatProvider provider, string format, params object[] args)
        {
            log.Fatal(string.Format(provider, format, args));
            return log;
        } 
        #endregion

        #region Info
        /// <summary>
        /// <see cref="MessageType.Info"/>级别
        /// </summary>
        /// <param name="log">日志记录器</param>
        /// <param name="message">日志消息</param>
        /// <param name="ex">异常</param>
        /// <returns>日志记录器</returns>
        public static ILog Info(this ILog log, string message, Exception ex = null)
        {

            log.Write(message, MessageType.Info, ex);

            return log;
        }

        /// <summary>
        /// 记录<see cref="MessageType.Info"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="format">格式化的字符串</param>
        /// <param name="args">参数集合</param>
        public static ILog InfoFormat(this ILog log, string format, params object[] args)
        {
            log.Info(string.Format(format, args));
            return log;
        }
        /// <summary>
        /// 记录<see cref="MessageType.Info"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="provider">字符串格式化提供程序</param>
        /// <param name="format">格式化的字符串</param>
        /// <param name="args">参数集合</param>
        public static ILog InfoFormat(this ILog log, IFormatProvider provider, string format, params object[] args)
        {
            log.Info(string.Format(provider, format, args));
            return log;
        } 
        #endregion

        #region Warn
        /// <summary>
        /// <see cref="MessageType.Warn"/>级别
        /// </summary>
        /// <param name="log">日志记录器</param>
        /// <param name="message">日志消息</param>
        /// <param name="ex">异常</param>
        /// <returns>日志记录器</returns>
        public static ILog Warn(this ILog log, string message, Exception ex = null)
        {

            log.Write(message, MessageType.Warn, ex);

            return log;
        }

        /// <summary>
        /// 记录<see cref="MessageType.Info"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="format">格式化的字符串</param>
        /// <param name="args">参数集合</param>
        public static ILog WarnFormat(this ILog log, string format, params object[] args)
        {
            log.Warn(string.Format(format, args));
            return log;
        }
        /// <summary>
        /// 记录<see cref="MessageType.Warn"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="provider">字符串格式化提供程序</param>
        /// <param name="format">格式化的字符串</param>
        /// <param name="args">参数集合</param>
        public static ILog WarnFormat(this ILog log, IFormatProvider provider, string format, params object[] args)
        {
            log.Warn(string.Format(provider, format, args));
            return log;
        }
        #endregion

        #region Trace
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="log"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static ILog Trace<T>(this ILog log, T obj) where T : class
        {

            if (obj != null)
                log.Write(Json.JsonHelper.Serialize(obj), MessageType.Trace);

            return log;
        }

        #endregion

        #region Write
        ///// <summary>
        ///// 写入以Info类型写入日志
        ///// </summary>
        ///// <param name="message">日志消息</param>
        public static void Write(this ILog log,string message)
        {
            log.Write(message, MessageType.Info);
        }

        ///// <summary>
        ///// 写入日志并指定写入类型
        ///// </summary>
        ///// <param name="message">日志消息</param>
        ///// <param name="messageType">日志类型</param>
        public static void Write(this ILog log,string message, MessageType messageType)
        {
            log.Write(message, messageType, null);
        }

        #endregion

        #region Assert
        /// <summary>
        /// 如果<paramref name="bl"/>为<see cref="Boolean.TrueString"/>则将<paramref name="message"/>以<see cref="MessageType.Info"/>类型写入日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="bl">为true则将<paramref name="message"/>写入日志</param>
        /// <param name="message">日志内容</param>
        public static void Assert(this ILog log, bool bl, string message)
        {
            log.Assert(bl, message, MessageType.Info);
        }

        /// <summary>
        /// 如果<paramref name="bl"/>为<see cref="Boolean.TrueString"/>则将<paramref name="message"/>以<paramref name="messageType"/>类型写入日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="bl">为true则将<paramref name="message"/>写入日志</param>
        /// <param name="message">日志内容</param>
        /// <param name="messageType">日志类型</param>
        public static void Assert(this ILog log, bool bl, string message, MessageType messageType)
        {
                log.Assert(bl,message,messageType,null);
        }
        /// <summary>
        /// 如果<paramref name="bl"/>为<see cref="Boolean.TrueString"/>则将<paramref name="message"/>以<paramref name="messageType"/>类型写入日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="bl">为true则将<paramref name="message"/>写入日志</param>
        /// <param name="message">日志内容</param>
        /// <param name="messageType">日志类型</param>
        /// <param name="ex"></param>
        public static void Assert(this ILog log, bool bl, string message, MessageType messageType,Exception ex)
        {
            if (bl)
                log.Write(message,messageType,ex);
        } 
        #endregion

    }
}
