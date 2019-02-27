using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SAE.CommonComponent.Upload.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.CommonComponent.Upload
{
    public static class DecoratorExtension
    {
        private static Queue<Type> queue = new Queue<Type>();
        /// <summary>
        /// 添加文件装饰器
        /// </summary>
        /// <typeparam name="TFileDecorator"></typeparam>
        /// <param name="serviceDescriptors"></param>
        /// <returns></returns>
        public static IServiceCollection AddFileDecorator<TFileDecorator>(this IServiceCollection serviceDescriptors)where TFileDecorator : class,IFileDecorator
        {
            serviceDescriptors.AddSingleton<TFileDecorator, TFileDecorator>();
            queue.Enqueue(typeof(TFileDecorator));
            serviceDescriptors.TryAddSingleton<IFileDecorator>(provider =>
            {
                var type = queue.Dequeue();
                var firstDecorator = provider.GetService(type) as FileDecorator;
                var parentDecorator = firstDecorator;
                while (queue.Any())
                {
                    type = queue.Dequeue();
                    var decorator = provider.GetService(type) as FileDecorator;
                    parentDecorator.SetDecorator(decorator);
                    parentDecorator = decorator;
                }
                return firstDecorator;
            });
            return serviceDescriptors;
        }
    }
}
