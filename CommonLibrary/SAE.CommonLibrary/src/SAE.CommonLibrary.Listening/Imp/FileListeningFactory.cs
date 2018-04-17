using SAE.CommonLibrary.Log;
using System.Collections.Generic;

namespace SAE.CommonLibrary.Listening.Imp
{
    /// <summary>
    /// 通过工厂和享元减少对象创建
    /// </summary>
    public class FileListeningFactory : IFileListeningFactory
    {
        private readonly object _lock = new object();
        private readonly IDictionary<string, IFileListening> _dic;
        private readonly ILog _log;
        /// <summary>
        /// 文件监听工厂
        /// </summary>
        /// <param name="log"></param>
        public FileListeningFactory(ILog<FileWatcher> log)
        {
            _dic = new Dictionary<string, IFileListening>();
            _log = log;
        }
        public IFileListening Create(string path)
        {
            return this.Create(path.Trim(), string.Empty);
        }
        public IFileListening Create(string path, string extension)
        {
            return this.Create(path.Trim(), extension.Trim(),Listen.Current);
        }

        public IFileListening Create(string path, string extension, Listen listen)
        {
            return this.Builder(path.Trim(), extension.Trim(),listen);
        }

        private IFileListening Builder(string path, string extension,Listen listen)
        {
            path=path.Replace("/", "\\");
            var key = path;
            if(!string.IsNullOrWhiteSpace(extension))
            {
                key += extension;
            }
            if (!_dic.ContainsKey(key))
            {
                lock (_lock)
                {
                    if (!_dic.ContainsKey(key))
                    {
                        _log.Info($"Create Watcher:path={path},extension={extension ?? ""}");
                        _dic[key] = new FileWatcher(path, extension, listen, this._log);
                    }
                }
            }
            return _dic[key];
        }
    }
}