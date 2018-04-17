using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.EventStore.Document
{
    public static class DocumentExtension
    {
        /// <summary>
        /// 添加EventStore.Docment默认实现
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddDocument(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddJson();
            serviceCollection.TryAddSingleton<IDocumentStore, DefaultDocumentStore>();
            return serviceCollection;
        }

        /// <summary>
        /// 从文档中获得文档对象
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static TDocument Find<TDocument>(this IDocumentStore documentStore,IIdentity identity) where TDocument : IDocument, new()
        {
            return documentStore.FindAsync<TDocument>(identity)
                                .GetAwaiter()
                                .GetResult();
        }

        /// <summary>
        /// 获取特点版本的文档对象
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="identity">标识</param>
        /// <param name="version">版本号</param>
        /// <returns></returns>
        public static TDocument Find<TDocument>(this IDocumentStore documentStore, IIdentity identity, long version) where TDocument : IDocument, new()
        {
            return documentStore.FindAsync<TDocument>(identity, version)
                                .GetAwaiter()
                                .GetResult();
        }

        /// <summary>
        /// 保存文件操作事件
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="document"></param>
        public static void Save(this IDocumentStore documentStore, IDocument document)
        {
            documentStore.SaveAsync(document)
                         .Wait();
        }

        public static void Append(this IDocumentEvent documentEvent,IDocument document, IEnumerable<IEvent> events)
        {
            documentEvent.AppendAsync(document, events)
                         .Wait();
        }
    }
}
