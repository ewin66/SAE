using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SAE.CommonLibrary.EventStore.Document
{
    /// <summary>
    /// 文档存储
    /// </summary>
    public interface IDocumentStore
    {
        /// <summary>
        /// 从文档中获得文档对象
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="identity"></param>
        /// <returns></returns>
        Task<TDocument> FindAsync<TDocument>(IIdentity identity) where TDocument : IDocument, new();

        /// <summary>
        /// 获取特点版本的文档对象
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="identity">标识</param>
        /// <param name="version">版本号</param>
        /// <returns></returns>
        Task<TDocument> FindAsync<TDocument>(IIdentity identity, long version) where TDocument : IDocument, new();

        /// <summary>
        /// 保存文件操作事件
        /// </summary>
        /// <param name="document"></param>
        Task SaveAsync(IDocument document);

        /// <summary>
        /// 移除文档
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        Task RemoveAsync(IDocument document);
        /// <summary>
        /// 使用<seealso cref="IIdentity"/>移除文档对象
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        Task RemoveAsync(IIdentity identity);
    }
}
