using SAE.CommonLibrary.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
namespace SAE.CommonLibrary.Listening.Imp
{
    /// <summary>
    /// 基于通知的文件监听
    /// </summary>
    public class FileWatcher : IFileListening
    {
        ~FileWatcher()
        {
            _log.Info("FileWatcher:已调用指定终结器");
            this.Dispose();
        }
        private Dictionary<string, DateTime> _fileChangeDate = new Dictionary<string, DateTime>();
        private readonly ILog _log;

        #region Private Property and Field

        /// <summary>
        /// 文件监听组件
        /// </summary>
        private readonly FileSystemWatcher _watcher;
        /// <summary>
        /// 路径是否是文件
        /// </summary>
        private bool isFile;

        #endregion Private Property and Field

        private string path;

        /// <summary>
        /// 需要监听的文件夹,不监听子目录,当为文件时则只监听文件
        /// </summary>
        public string Path
        {
            get
            {
                return path;
            }
            private set
            {
                path = value;
                if (!System.IO.Directory.Exists(path))
                {
                    isFile = System.IO.Path.HasExtension(path);
                }
                
            }
        }

        /// <summary>
        /// 扩展如果为null则监听所有文件
        /// </summary>
        public string Extension { get; private set; }
        /// <summary>
        /// 初始化一个监控
        /// </summary>
        /// <param name="path">监听路径</param>
        /// <param name="extension">扩展名</param>
        /// <param name="Listen">监听层次</param>
        /// <param name="log">日志</param>
        public FileWatcher(string path, string extension,Listen listen,ILog log)
        {
            _log = log;
            if (string.IsNullOrWhiteSpace(path))
            {
                _log.Error($"{nameof(path)}是必须的");
                throw new ArgumentNullException($"{nameof(path)}", $"{nameof(path)}是必须的");
            }
            this.Path = path;

            this.Extension = isFile?System.IO.Path.GetFileName(path):extension;

            _watcher = new FileSystemWatcher();
            if (isFile)
            {
                _watcher.Path = System.IO.Path.GetDirectoryName(this.Path);
                _watcher.IncludeSubdirectories = false;
            }
            else
            {
                _watcher.Path = this.Path;
                _watcher.IncludeSubdirectories = listen == Listen.All;
            }
            

            _watcher.NotifyFilter = NotifyFilters.LastWrite |
                                    NotifyFilters.FileName |
                                    NotifyFilters.DirectoryName |
                                    NotifyFilters.CreationTime;

            _watcher.Filter = string.IsNullOrWhiteSpace(this.Extension) ?"*.*": this.Extension;

            _watcher.Changed += Watcher_Changed;
            _watcher.Created += Watcher_Changed;
            _watcher.Deleted += Watcher_Changed;
            _watcher.Error += _watcher_Error;

            this._log.Info($"{nameof(_watcher.Path)}:{_watcher.Path}");
            this._log.Info($"{nameof(_watcher.Filter)}:{_watcher.Filter}");

            _watcher.EnableRaisingEvents = true;
            this._log.Info("Start Watcher");
        }

        private void _watcher_Error(object sender, ErrorEventArgs e)
        {
            _log.Error(e.GetException()?.Message, e.GetException());
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (!System.IO.Path.HasExtension(e.FullPath))
            {
                if (this._watcher.IncludeSubdirectories)
                {
                    switch (e.ChangeType)
                    {
                        case WatcherChangeTypes.Changed:
                            {
                                this._log.Debug($"File {e.FullPath} Trigger Change Event");
                                FileChangeEvent?.Invoke(e.FullPath);
                            }
                            break;

                        case WatcherChangeTypes.Created:
                            {
                                this._log.Debug($"File {e.FullPath} Trigger Create Event");
                                FileCreateEvent?.Invoke(e.FullPath);
                            }
                            break;

                        case WatcherChangeTypes.Deleted:
                            {
                                this._log.Debug($"File {e.FullPath} Trigger Remove Event");
                                FileRemoveEvent?.Invoke(e.FullPath);
                            }
                            break;
                    }
                }
                else
                {
                    this._log.Debug($"not exist {e.FullPath} file");
                }
                
                return;
            }
            
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Changed:
                    {
                        DateTime prevDate;
                        if(_fileChangeDate.TryGetValue(e.FullPath,out prevDate))
                        {
                            var currentDate=File.GetLastWriteTime(e.FullPath);

                            if (prevDate == currentDate) return;

                            _fileChangeDate[e.FullPath] = currentDate;
                        }
                        else
                        {
                            _fileChangeDate[e.FullPath] = DateTime.Now;
                        }
                        this._log.Debug($"File {e.FullPath} Trigger Change Event");
                        FileChangeEvent?.Invoke(e.FullPath);
                    }
                    break;

                case WatcherChangeTypes.Created:
                    {
                        this._log.Debug($"File {e.FullPath} Trigger Create Event");
                        FileCreateEvent?.Invoke(e.FullPath);
                    }
                    break;

                case WatcherChangeTypes.Deleted:
                    {
                        this._log.Debug($"File {e.FullPath} Trigger Remove Event");
                        FileRemoveEvent?.Invoke(e.FullPath);
                    }
                    break;
            }
        }


        public event Action<string> FileChangeEvent;

        public event Action<string> FileCreateEvent;

        public event Action<string> FileRemoveEvent;

#region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (!disposedValue)
            {
                this._log.Info("Dispose this");
                this._watcher.Dispose();
                disposedValue = true;
            }

            //如果调用了Dispose则取消终结器的调用
            GC.SuppressFinalize(this);
            
        }
#endregion
    }
}