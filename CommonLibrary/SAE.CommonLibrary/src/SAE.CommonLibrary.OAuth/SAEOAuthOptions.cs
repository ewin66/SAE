using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;

namespace SAE.CommonLibrary.OAuth
{
    /// <summary>
    /// 授权认证配置
    /// </summary>
    public class SAEOAuthOptions
    {

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ResponseType { get; set; } = OpenIdConnectResponseType.Code;
        public string Authority { get; set; } = OAuthDefault.AuthorizationBaseEndpoint;
        public Cookie Cookie { get; set; } = new Cookie();
        public PathString LoginPath { get; set; } = OAuthDefault.LoginPath;
        public PathString LogoutPath { get; set; } = OAuthDefault.LogoutPath;

        public string AuthenticationScheme = OAuthDefault.AuthenticationScheme;

        public string CookieAuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    }
    public class Cookie
    {
        public Cookie()
        {
            this.Name= CookieAuthenticationDefaults.CookiePrefix + "ClientCookie" ;
            this.ExpireTimeSpan = TimeSpan.FromMinutes(5D);
        }
        public string Name { get;set;}
        public TimeSpan ExpireTimeSpan { get; set; }
        
    }
}
