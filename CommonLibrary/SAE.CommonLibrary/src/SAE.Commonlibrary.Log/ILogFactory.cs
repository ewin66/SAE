namespace SAE.CommonLibrary.Log
{
    /// <summary>
    /// 日志工厂
    /// </summary>
    public interface ILogFactory
    {
        /// <summary>
        /// 创建默认的ILog
        /// </summary>
        /// <returns>返回默认的记录器"Default"</returns>
        ILog Create();

        /// <summary>
        /// 创建具有指定名称的ILog
        /// </summary>
        /// <param name="logName">记录器的名称</param>
        /// <returns>返回一个指定的记录器</returns>
        ILog Create(string logName);
        /// <summary>
        /// 根据类型创建Log器
        /// </summary>
        /// <typeparam name="TCategoryName">指定类名的log器</typeparam>
        /// <returns>返回一个指定的记录器TCategoryName</returns>
        ILog Create<TCategoryName>();
    }
}