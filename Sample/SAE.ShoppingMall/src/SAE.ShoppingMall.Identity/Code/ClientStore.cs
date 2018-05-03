using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using SAE.CommonLibrary.OAuth;
using SAE.ShoppingMall.Identity.Application;
using SAE.ShoppingMall.Identity.DocumentService;

namespace SAE.ShoppingMall.Identity.Code
{
    public class ClientStore : IClientStore
    {
        private readonly IAppService _appService;
        private readonly IAppQueryService _appQueryService;
        public ClientStore(IAppService appService,IAppQueryService appQueryService)
        {
            this._appService = appService;
            this._appQueryService = appQueryService;
        }
        public Task<Client> FindClientByIdAsync(string clientId)
        {
            var app = this._appService.Find(clientId);
            return Task.FromResult(new Client
            {
                ClientId = app.AppId,
                ClientName = app.Name,
                ClientSecrets = new List<Secret> { new Secret(app.AppSecret.Sha256()) },
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                AccessTokenType = AccessTokenType.Reference,
                AllowedScopes = new string[] { IdentityServerConstants.StandardScopes.Profile, IdentityServerConstants.StandardScopes.OpenId, OAuthDefault.ApiScope },
                RedirectUris = new string[] { app.Signin },
                RequireConsent = false,
                AccessTokenLifetime=OAuthDefault.ClientCertificationTokenLifetime,
            });
        }
    }
}
