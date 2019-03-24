using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SAE.CommonLibrary.MQ;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SAE.CommonLibrary.EventStore.Document
{
    /// <summary>
    /// 
    /// </summary>
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
            serviceCollection.TryAddSingleton<IDocumentEvent, DefaultDocumentEvent>();
            return serviceCollection;
        }

        /// <summary>
        /// 从文档中获得文档对象
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="documentStore"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static TDocument Find<TDocument>(this IDocumentStore documentStore, IIdentity identity) where TDocument : IDocument, new()
        {
            return documentStore.FindAsync<TDocument>(identity)
                                .GetAwaiter()
                                .GetResult();
        }

        /// <summary>
        /// 获取特点版本的文档对象
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="documentStore"></param>
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
        /// 保存文档对象
        /// </summary>
        /// <param name="documentStore"></param>
        /// <param name="document"></param>
        public static void Save(this IDocumentStore documentStore, IDocument document)
        {
            documentStore.SaveAsync(document)
                         .Wait();
        }

        /// <summary>
        /// 附加到文档
        /// </summary>
        /// <param name="documentEvent"></param>
        /// <param name="document"></param>
        /// <param name="events"></param>
        public static void Append(this IDocumentEvent documentEvent, IDocument document, IEnumerable<IEvent> events)
        {
            documentEvent.AppendAsync(document, events)
                         .Wait();
        }


        /// <summary>
        /// 从<seealso cref="IDocumentStore"/>中移除该对象
        /// </summary>
        /// <param name="documentStore"></param>
        /// <param name="identity"></param>
        public static void Remove(this IDocumentStore documentStore, IIdentity identity)
        {
            documentStore.RemoveAsync(identity)
                         .Wait();
        }

        /// <summary>
        /// 从<seealso cref="IDocumentStore"/>中移除该对象
        /// </summary>
        /// <param name="documentStore"></param>
        /// <param name="document"></param>
        public static void Remove<TDocument>(this IDocumentStore documentStore, TDocument document) where TDocument : IDocument, new()
        {
            documentStore.RemoveAsync(document)
                         .Wait();
        }

        /// <summary>
        /// 使用<seealso cref="DefaultDocumentEvent"/>发布器
        /// </summary>
        /// <param name="provider"></param>
        public static IServiceProvider UseDefaultDocumentPublish(this IServiceProvider provider)
        {
            var documentEvent = provider.GetService<IDocumentEvent>() as DefaultDocumentEvent;
            var mq = provider.GetService<IMQ>();
            documentEvent.OnAppend += async (document, events) =>
            {
                foreach (var @event in events)
                    await mq.PublishAsync(@event);
            };
            return provider;
        }
    }
}
