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
            //����һ��LogHelperTest����־��¼��
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
            //LogHelper��̬����д����ֲ�ε���Ϣ
            _log.Write($"���ԡ�{type}�����͵���Ϣ",type);
            //ʹ��LogHelperTest���͵���־��¼��д����Ϣ
            this._log.Write($"���ԡ�{type}�����͵���Ϣ", type);
        }
        [Fact]
        public void Info()
        {
            _log.Info("��¼һ����ϸ��Ϣ");
            this._log.Info("���{0},����{1},�ҽ���{2}����,-----{3}","pjb","cwj",24,new Student());
        }
        [Fact]
        public void Debug()
        {
            _log.Debug("��¼һ��������Ϣ");
            this._log.Debug("��¼һ��{0}��Ϣ{1}","����","---");
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
                _log.Error(error,"��¼һ���쳣��Ϣ");
            }
            
        }
        [Fact]
        public void Fatal()
        {
            this._log.Error(new Exception("����һ���Զ�����쳣��¼"));
            this._log.Error("�����ַ�ƴ��");
            _log.Fatal("��¼һ������������Ϣ");
        }
        [Fact]
        public void Warn()
        {
            _log.Warn("��¼һ��������Ϣ");
            //this._log.WarnFormat("�ǺǴ");
            //LogHelper.Warn<object>($"��¼һ��{nameof(Object)}���͵ĵľ�����Ϣ");
        }
        
    }
}
