
using SAE.CommonLibrary.OAuth;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.AspNetCore.Builder
{
    public static class SAEAppBuilderExtensions
    {
    
        /// <summary>
        /// 使用新的回发通道处理程序
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseBackchannelHandler(this IApplicationBuilder builder)
        {
            var handle=builder.ApplicationServices.GetService<HttpMessageHandler>();
            OAuthDefault.BackchannelHttpHandler = () => handle;
            
            if(handle is DelegatingHandler)
            {
                var delegatingHandler = handle as DelegatingHandler;

                OAuthDefault.BackChannelHandler = currentHandle =>
                {
                    delegatingHandler.InnerHandler = currentHandle;
                    return delegatingHandler;
                };
            }
            return builder;
        }
    }
}
