using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using SAE.ShoppingMall.Identity.Application;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SAE.ShoppingMall.Identity.Code
{
    public class ClientStore : IClientStore
    {
        private readonly IAppService _appService;
        public ClientStore(IAppService appService)
        {
            this._appService = appService;
        }
        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var app = this._appService.GetById(clientId);
           
            if (app == null) return null;
            return new Client
            {
                ClientId = app.AppId,
                ClientSecrets = new List<Secret>
               {
                   new Secret(app.AppSecret.Sha256())
               },
                AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                AllowedScopes = new[]
               {
                   OidcConstants.StandardScopes.OpenId,
                   OidcConstants.StandardScopes.Profile
               },
                RedirectUris = { app.Signin },
                PostLogoutRedirectUris = { app.Signout },
                AllowRememberConsent = false,
                RequireConsent = false,
                AlwaysIncludeUserClaimsInIdToken = true,
                
            };
        }
    }
}
