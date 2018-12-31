using SAE.CommonLibrary.Common;
using SAE.CommonLibrary.Json;
using SAE.CommonLibrary.Provider;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAE.CommonLibrary.Log
{
    /// <summary>
    /// 日志扩展
    /// </summary>
    public static class LogExtensions
    {
        private static readonly Lazy<IJsonConvertor> _jsonConvertor = new Lazy<IJsonConvertor>(ServiceFacade.Provider.GetService<IJsonConvertor>);

        #region Debug
         /// <summary>
        /// 记录<see cref="Level.Debug"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="message">格式化的字符串</param>
        public static ILog Debug(this ILog log, string message)
            => log.Write(Level.Debug, message);


        /// <summary>
        /// 记录<see cref="Level.Debug"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="format">格式化的字符串</param>
        /// <param name="args">参数集合</param>
        public static ILog Debug(this ILog log, string format, params object[] args)
            => log.Write(Level.Debug, format, args);

        /// <summary>
        /// 记录<see cref="Level.Debug"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="provider">字符串格式化提供程序</param>
        /// <param name="format">格式化的字符串</param>
        /// <param name="args">参数集合</param>
        public static ILog Debug(this ILog log, IFormatProvider provider, string format, params object[] args)
            => log.Write(Level.Debug, provider, format, args);
        #endregion

        #region Info

        /// <summary>
        /// 记录<see cref="Level.Info"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="message">格式化的字符串</param>
        public static ILog Info(this ILog log, string message)
            => log.Write(Level.Info, message);


        /// <summary>
        /// 记录<see cref="Level.Info"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="format">格式化的字符串</param>
        /// <param name="args">参数集合</param>
        public static ILog Info(this ILog log, string format, params object[] args)
            => log.Write(Level.Info, format, args);

        /// <summary>
        /// 记录<see cref="Level.Info"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="provider">字符串格式化提供程序</param>
        /// <param name="format">格式化的字符串</param>
        /// <param name="args">参数集合</param>
        public static ILog Info(this ILog log, IFormatProvider provider, string format, params object[] args)
            => log.Write(Level.Info, provider, format, args);

        #endregion

        #region Warn
        /// <summary>
        /// 记录<see cref="Level.Warn"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="message">格式化的字符串</param>
        public static ILog Warn(this ILog log, string message)
            => log.Write(Level.Warn, message);


        /// <summary>
        /// 记录<see cref="Level.Warn"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="format">格式化的字符串</param>
        /// <param name="args">参数集合</param>
        public static ILog Warn(this ILog log, string format, params object[] args)
            => log.Write(Level.Warn, format, args);

        /// <summary>
        /// 记录<see cref="Level.Warn"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="provider">字符串格式化提供程序</param>
        /// <param name="format">格式化的字符串</param>
        /// <param name="args">参数集合</param>
        public static ILog Warn(this ILog log, IFormatProvider provider, string format, params object[] args)
            => log.Write(Level.Warn, provider, format, args);
        #endregion

        #region Error

        /// <summary>
        /// 记录<see cref="Level.Error"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="exception">异常</param>
        public static ILog Error(this ILog log, Exception exception)
            => log.Write(string.Empty, Level.Error,exception:exception);

        /// <summary>
        /// 记录<see cref="Level.Error"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="message">格式化的字符串</param>
        public static ILog Error(this ILog log, string message)
            => log.Write(Level.Error, message);

        /// <summary>
        /// 记录<see cref="Level.Error"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="exception"></param>
        /// <param name="message">格式化的字符串</param>
        public static ILog Error(this ILog log,Exception exception ,string message)
            => log.Write(message,Level.Error,exception:exception);

        /// <summary>
        /// 记录<see cref="Level.Error"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="format">格式化的字符串</param>
        /// <param name="args">参数集合</param>
        public static ILog Error(this ILog log, string format, params object[] args)
            => log.Write(Level.Error, format, args);

        /// <summary>
        /// 记录<see cref="Level.Error"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="exception"></param>
        /// <param name="format">格式化的字符串</param>
        /// <param name="args">参数集合</param>
        public static ILog Error(this ILog log, Exception exception, string format, params object[] args)
            => log.Write(Level.Error,exception:exception,provider: null,format: format,args: args);

        /// <summary>
        /// 记录<see cref="Level.Error"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="provider">字符串格式化提供程序</param>
        /// <param name="format">格式化的字符串</param>
        /// <param name="args">参数集合</param>
        public static ILog Error(this ILog log, IFormatProvider provider, string format, params object[] args)
            => log.Write(Level.Error, provider, format, args);

        /// <summary>
        /// 记录<see cref="Level.Error"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="exception"></param>
        /// <param name="provider">字符串格式化提供程序</param>
        /// <param name="format">格式化的字符串</param>
        /// <param name="args">参数集合</param>
        public static ILog Error(this ILog log, Exception exception, IFormatProvider provider, string format, params object[] args)
            => log.Write(Level.Error,exception:exception,provider:provider,format: format,args: args);

        #endregion

        #region Fatal

        /// <summary>
        /// 记录<see cref="Level.Fatal"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="message">格式化的字符串</param>
        public static ILog Fatal(this ILog log, string message)
            => log.Write(Level.Fatal, message);


        /// <summary>
        /// 记录<see cref="Level.Fatal"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="format">格式化的字符串</param>
        /// <param name="args">参数集合</param>
        public static ILog Fatal(this ILog log, string format, params object[] args)
            => log.Write(Level.Fatal, format, args);

        /// <summary>
        /// 记录<see cref="Level.Fatal"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="provider">字符串格式化提供程序</param>
        /// <param name="format">格式化的字符串</param>
        /// <param name="args">参数集合</param>
        public static ILog Fatal(this ILog log, IFormatProvider provider, string format, params object[] args)
            => log.Write(Level.Fatal, provider, format, args);
        #endregion

        #region Trace

        /// <summary>
        /// 记录<see cref="Level.Trace"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="message">格式化的字符串</param>
        public static ILog Trace(this ILog log, string message)
            => log.Write(Level.Trace, message);


        /// <summary>
        /// 记录<see cref="Level.Trace"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="format">格式化的字符串</param>
        /// <param name="args">参数集合</param>
        public static ILog Trace(this ILog log, string format, params object[] args)
            => log.Write(Level.Trace, format, args);

        /// <summary>
        /// 记录<see cref="Level.Trace"/>级别日志
        /// </summary>
        /// <param name="log">日志接口</param>
        /// <param name="provider">字符串格式化提供程序</param>
        /// <param name="format">格式化的字符串</param>
        /// <param name="args">参数集合</param>
        public static ILog Trace(this ILog log, IFormatProvider provider, string format, params object[] args)
            => log.Write(Level.Trace, provider, format, args);

        #endregion

        #region Write
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="level"></param>
        /// <param name="exception"></param>
        /// <param name="provider"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static ILog Write(this ILog log, Level level, Exception exception, IFormatProvider provider, string format, params object[] args)
        {

            if (format.IsNullOrWhiteSpace() || !log.IsEnabled(level))
                return log;

            var message = format;

            if (args != null && args.Any())
            {
                string[] strArray = new string[args.Length];

                for(var i=0; i < args.Length; i++)
                {
                    var arg = args[i];
                    if(arg is string)
                    {
                        strArray.SetValue(arg, i);
                    }
                    else
                    {
                        strArray.SetValue(_jsonConvertor.Value?.Serialize(arg) ?? arg.ToString(), i);
                    }
                }

                if (provider == null)
                {
                    message = string.Format(message, strArray);
                }
                else
                {
                    message = string.Format(provider, message, strArray);
                }
            }

            return log.Write(message, level,exception:exception);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="level"></param>
        /// <param name="provider"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static ILog Write(this ILog log, Level level, IFormatProvider provider, string format, params object[] args) 
            => log.Write(level:level,exception:null,provider:provider,format: format,args:args);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="level"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static ILog Write(this ILog log, Level level, string format, params object[] args)
            => log.Write(level: level, exception: null, provider: null, format: format, args: args);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="message"></param>
        /// <param name="level"></param>
        public static ILog Write(this ILog log, string message, Level level)
            => log.Write(message: message,level: level,exception:null);
        #endregion

    }
}
