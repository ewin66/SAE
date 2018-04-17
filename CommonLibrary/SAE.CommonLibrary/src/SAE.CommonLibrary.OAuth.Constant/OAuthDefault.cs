using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SAE.CommonLibrary.OAuth
{
    public class OAuthDefault
    {

        /// <summary>
        /// 退出终端
        /// </summary>
        public const string LogOutEndpoint = AuthorizationBaseEndpoint+"/account/logout";

        public const string AuthorizationBaseEndpoint ="http://oauth.sae.com";
        /// <summary>
        /// 授权端点
        /// </summary>
        public const string AuthorizationEndpoint = AuthorizationBaseEndpoint+"/connect/authorize";
        /// <summary>
        /// Token授权端点
        /// </summary>
        public const string TokenEndpoint = AuthorizationBaseEndpoint+"/connect/token";
        /// <summary>
        /// 用户信息端点
        /// </summary>
        public const string UserInformationEndpoint = AuthorizationBaseEndpoint+"/connect/userinfo";
        /// <summary>
        /// 登录路径
        /// </summary>
        public const string LoginPath = "/signin";
        /// <summary>
        /// 登出路径
        /// </summary>
        public const string LogoutPath = "/signout";

        /// <summary>
        /// 认证方案
        /// </summary>
        public const string AuthenticationScheme = "SAE";
        /// <summary>
        /// 默认域
        /// </summary>
        public const string Scope = "openid";
        /// <summary>
        /// api域
        /// </summary>
        public const string ApiScope = "api";
        /// <summary>
        /// 发行人
        /// </summary>
        public const string Issuer = "SAE";
        /// <summary>
        /// 登出cookie
        /// </summary>
        public const string SingoutCookie = Issuer + ".singout";
        /// <summary>
        /// 用户token超时时间(一天)
        /// </summary>
        public const int UserTokenLifetime = 60 * 60 * 24 * 30;
        /// <summary>
        /// 客户端认证Token超时时间(30分钟)
        /// </summary>
        public const int ClientCertificationTokenLifetime = 60 * 30;

        public static Func<HttpMessageHandler, HttpMessageHandler> BackChannelHandler = handle => handle;
        public static Func<HttpMessageHandler> BackchannelHttpHandler = () => new HttpClientHandler();
    }
}
