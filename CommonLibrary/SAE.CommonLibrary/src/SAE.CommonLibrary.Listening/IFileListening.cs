using System;

namespace SAE.CommonLibrary.Listening
{
    /// <summary>
    /// 文件监听接口
    /// </summary>
    public interface IFileListening : IDisposable
    {

        /// <summary>
        /// 目录地址
        /// </summary>
        string Path { get; }

        /// <summary>
        /// 需要监听的文件格式
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// 文件改变事件
        /// </summary>
        event Action<string> FileChangeEvent;

        /// <summary>
        /// 文件创建事件
        /// </summary>
        event Action<string> FileCreateEvent;

        /// <summary>
        /// 文件移除事件(目前尚未实现)
        /// </summary>
        event Action<string> FileRemoveEvent;
    }
}