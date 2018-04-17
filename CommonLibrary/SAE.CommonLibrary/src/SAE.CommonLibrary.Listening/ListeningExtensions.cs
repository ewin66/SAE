#if !NET45
using SAE.CommonLibrary.Listening;
using SAE.CommonLibrary.Listening.Imp;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ListeningExtensions
    {
        /// <summary>
        /// 添加监控
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddListening(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IFileListeningFactory, FileListeningFactory>();
            serviceCollection.AddLogger();
            return serviceCollection;
        }
    }
}
#endif