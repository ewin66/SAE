using SAE.CommonLibrary.Log;
using SAE.CommonLibrary.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4;

namespace Microsoft.AspNetCore.Builder
{
    public static class SAEAppBuilderExtensions
    {


        /// <summary>
        /// 授权认证
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSAEAPIAuthentication(this IApplicationBuilder builder,
                                                                   string clientId,
                                                                   string clientSecret)
        {
#warning 未完成
            //builder.UseIdentityServer(new IdentityServerAuthenticationOptions
            //{
            //    Authority = OAuthDefault.AuthorizationBaseEndpoint,
            //    RequireHttpsMetadata = false,
            //    ApiName = clientId,
            //    ApiSecret = clientSecret,
            //    SupportedTokens = IdentityServer4.AccessTokenValidation.SupportedTokens.Reference,
            //    //ValidateScope = true,
            //    IntrospectionBackChannelHandler = OAuthDefault.BackChannelHandler(new CustomHttpMessageHandle(builder.ApplicationServices)),
            //    //AllowedScopes = new List<string> { OAuthDefault.ApiScope }
            //});

            return builder;
        }
        private class CustomHttpMessageHandle : HttpClientHandler
        {
            readonly IServiceProvider _serviceProvider;
            readonly ILog _log;
            public CustomHttpMessageHandle(IServiceProvider serviceProvider)
            {
                _serviceProvider = serviceProvider;
                _log = _serviceProvider.GetService<ILog<CustomHttpMessageHandle>>();
            }
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var context = _serviceProvider.GetService<IHttpContextAccessor>().HttpContext;
                if (context == null)
                {
                    _log.Error("HttpContext Not Exist");
                    throw new NullReferenceException("HttpContext Not Exist");
                }
                request.Headers.Referrer = new Uri($"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}");
                _log.Debug($"{nameof(request.Headers.Referrer)}:{request.Headers.Referrer}");

                _log.Debug("send request");

                return base.SendAsync(request, cancellationToken);
            }
        }
    }
}
