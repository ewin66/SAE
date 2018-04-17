using SAE.Test.Infrastructure;
using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using SAE.CommonLibrary.Json;

namespace SAE.CommonLibrary.Log.Test
{
    public class LogHelperTest
    {
        private readonly ILog _log;
        public LogHelperTest()
        {
            //-----------------------------------------------------------------------------------------//
            //创建一个LogHelperTest的日志记录器
            this._log = Unit.GetProvider(server=>server.AddLogger())
                            .GetService<ILog<LogHelperTest>>();
        }
        [Theory]
        [InlineData(MessageType.Debug)]
        [InlineData(MessageType.Error)]
        [InlineData(MessageType.Fatal)]
        [InlineData(MessageType.Info)]
        [InlineData(MessageType.Warn)]
        public void Write(MessageType type)
        {
            //LogHelper静态函数写入各种层次的信息
            LogHelper.Write($"测试“{type}”类型的消息",type);
            //使用LogHelperTest类型的日志记录器写入信息
            this._log.Write($"测试“{type}”类型的消息", type);
            //使用object类型的记录器写入信息
            LogHelper.Write<object>($"LogHelperTest“{type}” 类型的 消息", type);
        }
        [Fact]
        public void Info()
        {
            LogHelper.Info("记录一条详细信息");
            this._log.InfoFormat("你好{0},我是{1},我今年{2}岁了,-----{3}","pjb","cwj",24,new Student());
            LogHelper.Info<object>($"记录一条{nameof(Object)}类型的的详细信息");
        }
        [Fact]
        public void Debug()
        {
            LogHelper.Debug("记录一条调试信息");
            this._log.DebugFormat("记录一条{0}信息{1}","调试","---");
            LogHelper.Debug<object>($"记录一条{nameof(Object)}类型的的调试信息");
        }
        [Fact]
        public void Error()
        {
            var ex = new ArgumentNullException(paramName: "Student.Name");
            try
            {
                throw ex;
            }catch(Exception error)
            {
                LogHelper.Error("记录一条异常信息", error);
                LogHelper.Error<object>($"记录一条{nameof(Object)}类型的的异常信息", error);
            }
            
        }
        [Fact]
        public void Fatal()
        {
            this._log.Error(new Exception("这是一个自定义的异常记录"));
            this._log.ErrorFormat("这是字符拼接");
            LogHelper.Fatal("记录一条致命错误信息");
            LogHelper.Fatal<object>($"记录一条{nameof(Object)}类型的的致命错误信息");
        }
        [Fact]
        public void Warn()
        {
            LogHelper.Warn("记录一条警告信息");
            //this._log.WarnFormat("呵呵达。");
            //LogHelper.Warn<object>($"记录一条{nameof(Object)}类型的的警告信息");
        }
        
    }
}
