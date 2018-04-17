using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SAE.CommonLibrary.OAuth;
using System;
using System.Security.Claims;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 授权认证扩展
    /// </summary>
    public static class SAEAppBuilderExtensions
    {
      

        /// <summary>
        /// 使用华跃博弈授权认证
        /// </summary>
        /// <param name="service"></param>
        /// <param name="clientId">应用标识</param>
        /// <param name="clientSecret">密钥</param>
        /// <returns></returns>
        public static IServiceCollection AddSAEAuthentication(this IServiceCollection services, string clientId,string clientSecret)
        {
           return services.AddSAEAuthentication(s=> 
           {
               s.ClientId = clientId;
               s.ClientSecret = clientSecret;
           });
        }

        /// <summary>
        /// 授权认证
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceCollection AddSAEAuthentication(this IServiceCollection services, Action<SAEOAuthOptions> action)
        {
            var option = new SAEOAuthOptions();
            action(option);

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

#if DEBUG
            services.AddMvcCore(options =>
            {                
                options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));
            });
#endif
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = option.CookieAuthenticationScheme;
                options.DefaultSignInScheme = option.CookieAuthenticationScheme;
                options.DefaultChallengeScheme =option.AuthenticationScheme;
                
            }).AddCookie(options =>
            {
                options.Cookie.Name = option.Cookie.Name;
                options.ExpireTimeSpan = option.Cookie.ExpireTimeSpan;
                options.LoginPath = option.LoginPath;
                options.LogoutPath = option.LogoutPath;
            }).AddOpenIdConnect(OAuthDefault.AuthenticationScheme, options =>
             {
                 options.RequireHttpsMetadata = false;
                 options.SaveTokens = true;
                 options.GetClaimsFromUserInfoEndpoint = true;
                 options.ClientId = option.ClientId;
                 options.ClientSecret = option.ClientSecret;
                 options.ClaimActions.Remove("name");
                 options.ClaimActions.MapUniqueJsonKey(ClaimTypes.Name, "name");
                 options.ResponseType = option.ResponseType;
                 options.Authority = option.Authority;
             });
            return services;
        }


    }
}
