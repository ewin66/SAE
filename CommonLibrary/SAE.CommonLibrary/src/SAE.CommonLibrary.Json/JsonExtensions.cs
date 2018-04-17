﻿#if !NET45
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SAE.CommonLibrary.Json;
using SAE.CommonLibrary.Json.Imp;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class JsonExtensions
    {
        /// <summary>
        /// 添加Json
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddJson(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IJsonConvertor, JsonConvertor>();
            return serviceCollection;
        }
    }
}
#endif