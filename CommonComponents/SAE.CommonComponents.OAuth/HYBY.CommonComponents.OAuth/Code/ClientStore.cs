using SAE.CommonComponents.OAuth.Application;
using SAE.CommonLibrary.OAuth;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SAE.CommonComponents.OAuth.Code
{
    internal class ClientStore : IClientStore
    {
        private readonly IOAuthServer _oauthServer;
        public ClientStore(IOAuthServer oauthServer)
        {
            _oauthServer = oauthServer;
        }
        public Task<Client> FindClientByIdAsync(string clientId)
        {
            var app=_oauthServer.GetByClientId(clientId);

            if (app == null)
                return Task.FromResult<Client>(null);

            return Task.FromResult(new Client
            {
                ClientId = app.Client.Id,
                ClientName = app.Name,
                ClientSecrets = new List<Secret> { new Secret(app.Client.Secret.Sha256()) },
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                AccessTokenType = AccessTokenType.Reference,
                AllowedScopes = new string[] { IdentityServerConstants.StandardScopes.Profile,IdentityServerConstants.StandardScopes.OpenId,OAuthDefault.ApiScope },
                RedirectUris = new string[] { app.Signin },
                RequireConsent = false,
            });
            
        }
    }
}
