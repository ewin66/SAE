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
            //����һ��LogHelperTest����־��¼��
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
            //LogHelper��̬����д����ֲ�ε���Ϣ
            LogHelper.Write($"���ԡ�{type}�����͵���Ϣ",type);
            //ʹ��LogHelperTest���͵���־��¼��д����Ϣ
            this._log.Write($"���ԡ�{type}�����͵���Ϣ", type);
            //ʹ��object���͵ļ�¼��д����Ϣ
            LogHelper.Write<object>($"LogHelperTest��{type}�� ���͵� ��Ϣ", type);
        }
        [Fact]
        public void Info()
        {
            LogHelper.Info("��¼һ����ϸ��Ϣ");
            this._log.InfoFormat("���{0},����{1},�ҽ���{2}����,-----{3}","pjb","cwj",24,new Student());
            LogHelper.Info<object>($"��¼һ��{nameof(Object)}���͵ĵ���ϸ��Ϣ");
        }
        [Fact]
        public void Debug()
        {
            LogHelper.Debug("��¼һ��������Ϣ");
            this._log.DebugFormat("��¼һ��{0}��Ϣ{1}","����","---");
            LogHelper.Debug<object>($"��¼һ��{nameof(Object)}���͵ĵĵ�����Ϣ");
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
                LogHelper.Error("��¼һ���쳣��Ϣ", error);
                LogHelper.Error<object>($"��¼һ��{nameof(Object)}���͵ĵ��쳣��Ϣ", error);
            }
            
        }
        [Fact]
        public void Fatal()
        {
            this._log.Error(new Exception("����һ���Զ�����쳣��¼"));
            this._log.ErrorFormat("�����ַ�ƴ��");
            LogHelper.Fatal("��¼һ������������Ϣ");
            LogHelper.Fatal<object>($"��¼һ��{nameof(Object)}���͵ĵ�����������Ϣ");
        }
        [Fact]
        public void Warn()
        {
            LogHelper.Warn("��¼һ��������Ϣ");
            //this._log.WarnFormat("�ǺǴ");
            //LogHelper.Warn<object>($"��¼һ��{nameof(Object)}���͵ĵľ�����Ϣ");
        }
        
    }
}
