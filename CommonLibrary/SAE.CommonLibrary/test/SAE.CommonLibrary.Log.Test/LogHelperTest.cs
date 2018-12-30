using Microsoft.Extensions.DependencyInjection;
using SAE.Test.Infrastructure;
using System;
using Xunit;

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
                            .UseServiceFacade()
                            .GetService<ILog<LogHelperTest>>();
        }
        [Theory]
        [InlineData(Level.Debug)]
        [InlineData(Level.Error)]
        [InlineData(Level.Fatal)]
        [InlineData(Level.Info)]
        [InlineData(Level.Warn)]
        public void Write(Level type)
        {
            //LogHelper静态函数写入各种层次的信息
            _log.Write($"测试“{type}”类型的消息",type);
            //使用LogHelperTest类型的日志记录器写入信息
            this._log.Write($"测试“{type}”类型的消息", type);
        }
        [Fact]
        public void Info()
        {
            _log.Info("记录一条详细信息");
            this._log.Info("你好{0},我是{1},我今年{2}岁了,-----{3}","pjb","cwj",24,new Student());
        }
        [Fact]
        public void Debug()
        {
            _log.Debug("记录一条调试信息");
            this._log.Debug("记录一条{0}信息{1}","调试","---");
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
                _log.Error(error,"记录一条异常信息");
            }
            
        }
        [Fact]
        public void Fatal()
        {
            this._log.Error(new Exception("这是一个自定义的异常记录"));
            this._log.Error("这是字符拼接");
            _log.Fatal("记录一条致命错误信息");
        }
        [Fact]
        public void Warn()
        {
            _log.Warn("记录一条警告信息");
            //this._log.WarnFormat("呵呵达。");
            //LogHelper.Warn<object>($"记录一条{nameof(Object)}类型的的警告信息");
        }
        
    }
}
