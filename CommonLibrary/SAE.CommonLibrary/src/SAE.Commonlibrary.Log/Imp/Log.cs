using NLog;
using System;
using System.Reflection;
using System.Threading.Tasks;

#if NET45
using System.Threading;
#endif
namespace SAE.CommonLibrary.Log.Imp
{
    /// <summary>
    /// 通用日志类（除去老版本函数，其余均使用异步方式运行）
    /// </summary>
    public class Log : ILog
    {
        #region Private Readonly Field;

        private readonly ILogger _log;


        /// <summary>
        /// 从线程池中拿去一个可用线程异步执行
        /// </summary>
        private readonly Action<Action> _asyncExecute;

        #endregion 

        #region Ctor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public Log(string name = Unit.Default)
        {
            
            _log = LogManager.GetLogger(name);
            _asyncExecute = action =>
            {
                Task.Run(action);
            };
        }

        #endregion Ctor

        #region Log Member
        

        ///<summary>
        ///写日志
        ///</summary>
        ///<param name="message">日志消息</param>
        ///<param name="messageType">日志类型</param>
        ///<param name="ex">异常</param>
        public void Write(string message, MessageType messageType, Exception ex)
        {
            //_asyncExecute(() =>
            //{
            //    WriteLog(message, messageType, ex);
            //});
            WriteLog(message, messageType, ex);
        }

        //<summary>
        //写日志
        //</summary>
        //<param name="message">日志消息</param>
        //<param name="messageType">日志类型</param>
        //<param name="ex">异常</param>
        private void WriteLog(string message, MessageType messageType, Exception ex)
        {
            //_asyncExecute(() =>
            //{
            //    LogLevel logLevel;

            //    switch (messageType)
            //    {
            //        case MessageType.Debug: logLevel = LogLevel.Debug; break;
            //        case MessageType.Warn: logLevel = LogLevel.Warn; break;
            //        case MessageType.Error: logLevel = LogLevel.Error; break;
            //        case MessageType.Fatal: logLevel = LogLevel.Fatal; break;
            //        case MessageType.Trace: logLevel = LogLevel.Trace; break;
            //        default: logLevel = LogLevel.Info; break;
            //    }

            //    if (ex != null)
            //    {
            //        var exStr = Json.JsonHelper.Serialize(ex);
            //        if (!string.IsNullOrWhiteSpace(message))
            //        {
            //            message = $"{{ \"message\":\"{message}\",\"error\":{exStr} }}";
            //        }
            //        else
            //        {
            //            message = exStr;
            //        }
            //    }
            //    _log.Log(logLevel, message);
            //});

            LogLevel logLevel;

            switch (messageType)
            {
                case MessageType.Debug: logLevel = LogLevel.Debug; break;
                case MessageType.Warn: logLevel = LogLevel.Warn; break;
                case MessageType.Error: logLevel = LogLevel.Error; break;
                case MessageType.Fatal: logLevel = LogLevel.Fatal; break;
                case MessageType.Trace: logLevel = LogLevel.Trace; break;
                default: logLevel = LogLevel.Info; break;
            }

            //if (ex != null)
            //{
            //    var exStr = Json.JsonHelper.Serialize(ex);
            //    if (!string.IsNullOrWhiteSpace(message))
            //    {
            //        message = $"{{ \"message\":\"{message}\",\"error\":{exStr} }}";
            //    }
            //    else
            //    {
            //        message = exStr;
            //    }
            //}
            _log.Log(logLevel, ex, message);
        }
        #endregion Log Member
    }
}