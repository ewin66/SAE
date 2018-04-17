namespace SAE.CommonLibrary.Listening
{
    /// <summary>
    /// 监听
    /// </summary>
    public enum Listen
    {
        /// <summary>
        /// 当前
        /// </summary>
        Current,
        /// <summary>
        /// 所有
        /// </summary>
        All
    }
    /// <summary>
    /// 文件监听工厂
    /// </summary>
    public interface IFileListeningFactory
    {
        /// <summary>
        /// 根据指定路径创建<see cref="IFileListening"/>
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>返回<see cref="IFileListening"/>实例</returns>
        IFileListening Create(string path);
        /// <summary>
        /// 根据指定路径和扩展名<see cref="IFileListening"/>
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="extension">扩展</param>
        /// <returns>返回<see cref="IFileListening"/>实例</returns>
        IFileListening Create(string path, string extension);
        /// <summary>
        /// 根据指定路径和扩展名<see cref="IFileListening"/>
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="extension">扩展</param>
        /// <param name="listen"></param>
        /// <returns>返回<see cref="IFileListening"/>实例</returns>
        IFileListening Create(string path, string extension,Listen listen);
    }
}