using System;
using System.IO;
using System.Threading;
using SAE.CommonLibrary.Listening.Imp;
using Xunit;
using Xunit.Abstractions;
using SAE.Test.Infrastructure;
using SAE.CommonLibrary.NetCore;
using Microsoft.Extensions.DependencyInjection;

namespace SAE.CommonLibrary.Listening.Test
{
    public class FileListeningTest:IDisposable
    {
        private readonly IFileListening _fileListening;
        private readonly ITestOutputHelper _output;
        private readonly string _root;
        public FileListeningTest(ITestOutputHelper output)
        {
            _root = Utils.BaseDirectory();
            var factory = Unit.GetProvider(s=>s.AddListening())
                              .GetService<IFileListeningFactory>();

            //监听程序运行时,根目录的所有txt文本.
            _fileListening = factory.Create(_root, "*.txt");

           
            _output = output;
        }
        [Fact]
        public void Change()
        {
            var tempFile = Path.Combine(_root, "fileListeningTest.txt");
            _fileListening.FileChangeEvent += _fileListening_FileChangeEvent;
            File.AppendAllText(tempFile, "这是一个测试文件");
            Thread.Sleep(1000 * 2);
            File.Delete(tempFile);
        }

        [Fact]
        public void Create()
        {
            this._fileListening.FileCreateEvent += _fileListening_FileCreateEvent;
            var file = Path.Combine(_root, "createFileListening.txt");
            File.AppendAllText(file,"创建");
            Thread.Sleep(1000 * 2);
            File.Delete(file);
        }
        [Fact]
        public void Remove()
        {
            this._fileListening.FileRemoveEvent += _fileListening_FileRemoveEvent;
            var file = Path.Combine(_root, "createRemoveFileListening.txt");
            File.AppendAllText(file, "移除");
            File.Delete(file);
            Thread.Sleep(1000 * 2);
            
        }

        private void _fileListening_FileRemoveEvent(string obj)
        {
            _output.WriteLine($"移除了文件{obj}");
        }

        private void _fileListening_FileCreateEvent(string obj)
        {
            _output.WriteLine($"创建了文件{obj}");
        }

        private void _fileListening_FileChangeEvent(string obj)
        {
            _output.WriteLine($"更改了文件{obj}");
        }

        public void Dispose()
        {
            this._fileListening.Dispose();
        }
    }
}
