using SAE.CommonLibrary.Log;
using zipkin4net;

namespace SAE.CommonLibrary.Trace
{
    /// <summary>
    /// 跟踪日志
    /// </summary>
    public class TraceLogger : ILogger
    {
        
        private readonly ILog _log;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        public TraceLogger(ILog<TraceLogger> log)
        {
            _log = log;
        }
        /// <summary>
        /// 异常日志
        /// </summary>
        /// <param name="message"></param>
        public void LogError(string message)
        {
            _log.Error(message);
        }
        /// <summary>
        /// 信息日志
        /// </summary>
        /// <param name="message"></param>
        public void LogInformation(string message)
        {
            _log.Info(message);
        }
        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="message"></param>
        public void LogWarning(string message)
        {
            _log.Warn(message);
        }
    }
}
