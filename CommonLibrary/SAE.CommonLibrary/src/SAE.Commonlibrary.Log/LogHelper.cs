

using SAE.CommonLibrary.Json;
using NLog;
using System;
using System.Threading.Tasks;

namespace SAE.CommonLibrary.Log
{
    public class LogHelper
    {
        private  static readonly ILogFactory _logFactory;
        /// <summary>
        /// 获取<see cref="ILog"/>实例
        /// </summary>
        /// <param name="logName">日志收集器名称</param>
        /// <returns></returns>
        public static ILog GetLogger(string logName)
        {
            return _logFactory.Create(logName);
        }

        /// <summary>
        /// 默认的Log记录器
        /// </summary>
       
        static LogHelper()
        {
            Unit.Init();
            _logFactory = new Imp.LogFactory();
        }

        ///<summary>
        ///写日志
        ///</summary>
        ///<param name="message">日志消息</param>
        ///<param name="messageType">日志类型</param>
        ///<param name="ex">异常</param>
        public static void Write(string message, MessageType messageType=MessageType.Info, Exception ex=null)
        {
            WriteLog(message, messageType, ex);
        }



        ///<summary>
        ///写日志
        ///</summary>
        /// <typeparam name="TCategory">记录器类型</typeparam>
        ///<param name="message">日志消息</param>
        ///<param name="messageType">日志类型</param>
        ///<param name="ex">异常</param>
        public static void Write<TCategory>(string message, MessageType messageType = MessageType.Info, Exception ex = null)
        {
            WriteLog<TCategory>(message, messageType, ex);
        }


        ///<summary>
        ///断言
        ///</summary>
        ///<param name="condition">条件</param>
        ///<param name="message">日志信息</param>
        public static void Assert(bool condition, string message,MessageType messageType= MessageType.Info)
        {
            if (!condition)
            {
                WriteLog(message, messageType, null);
            }
        }

        /// <summary>
        /// 调试级别日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="ex">异常</param>
        public static void Debug(string message, Exception ex = null)
        {

            WriteLog(message, MessageType.Debug, ex);

        }
        /// <summary>
        /// 调试级别日志
        /// </summary>
        /// <typeparam name="TCategory">记录器类型</typeparam>
        /// <param name="message">日志消息</param>
        /// <param name="ex">异常</param>
        public static void Debug<TCategory>(string message, Exception ex = null)
        {

            WriteLog<TCategory>(message, MessageType.Debug, ex);

        }

        /// <summary>
        /// 异常级别日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="ex">异常</param>
        public static void  Error(string message, Exception ex = null)
        {

            WriteLog(message, MessageType.Error, ex);

        }

        /// <summary>
        /// 异常级别日志
        /// </summary>
        /// <typeparam name="TCategory">记录器类型</typeparam>
        /// <param name="message">日志消息</param>
        /// <param name="ex">异常</param>
        public static void Error<TCategory>(string message, Exception ex = null)
        {

            WriteLog<TCategory>(message, MessageType.Error, ex);
        }

        /// <summary>
        /// 致命错误级别
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="ex">异常</param>
        public static void Fatal(string message, Exception ex = null)
        {

            WriteLog(message, MessageType.Fatal, ex);
        }

        /// <summary>
        /// 致命错误级别
        /// </summary>
        /// <typeparam name="TCategory">记录器类型</typeparam>
        /// <param name="message">日志消息</param>
        /// <param name="ex">异常</param>
        public static void Fatal<TCategory>(string message, Exception ex = null)
        {

            WriteLog<TCategory>(message, MessageType.Fatal, ex);
        }

        /// <summary>
        /// 信息级别
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="ex">异常</param>
        public static void Info(string message, Exception ex = null)
        {
            WriteLog(message, MessageType.Info, ex);
        }

        /// <summary>
        /// 信息级别
        /// </summary>
        /// <typeparam name="TCategory">记录器类型</typeparam>
        /// <param name="message">日志消息</param>
        /// <param name="ex">异常</param>
        public static void Info<TCategory>(string message, Exception ex = null)
        {
            WriteLog<TCategory>(message, MessageType.Info, ex);
        }

        /// <summary>
        /// 警告级别
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="ex">异常</param>
        public static void Warn(string message, Exception ex = null)
        {
            WriteLog(message, MessageType.Warn, ex);
        }

        /// <summary>
        /// 警告级别
        /// </summary>
        /// <typeparam name="TCategory">记录器类型</typeparam>
        /// <param name="message">日志消息</param>
        /// <param name="ex">异常</param>
        public static void Warn<TCategory>(string message, Exception ex = null)
        {
            WriteLog<TCategory>(message, MessageType.Warn, ex);
        }

        /// <summary>
        /// 跟踪级别
        /// </summary>
        /// <param name="obj">跟踪对象</param>
        public static void Trace<T>(T obj)where T:class
        {
            if (obj == null) return;

            WriteLog(JsonHelper.Serialize(obj), MessageType.Trace,null);
        }


        #region Private

        //<summary>
        //写日志
        //</summary>
        //<param name="message">日志消息</param>
        //<param name="messageType">日志类型</param>
        //<param name="ex">异常</param>
        private static void WriteLog(string message, MessageType messageType, Exception ex)
        {
            _logFactory.Create().Write(message, messageType, ex);
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <typeparam name="TCatetory">记录器类型</typeparam>
        /// <param name="message">日志消息</param>
        /// <param name="messageType">日志类型</param>
        /// <param name="ex">异常</param>
        private static void WriteLog<TCatetory>(string message, MessageType messageType, Exception ex)
        {
            _logFactory.Create<TCatetory>().Write(message, messageType, ex);
        } 
        #endregion

    }

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum MessageType
    {
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
        Fatal,
        /// <summary>
        /// 跟踪
        /// </summary>
        Trace
    }
}