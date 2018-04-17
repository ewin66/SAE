using SAE.CommonComponents.OAuth.Application;
using SAE.CommonComponents.OAuth.Application.Dtos;
using SAE.CommonLibrary.OAuth;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace SAE.CommonComponents.OAuth.Code
{
    public class TokenStore : IReferenceTokenStore
    {
        private readonly IOAuthServer _oAuthServer;
        public TokenStore(IOAuthServer oAuthServer)
        {
            _oAuthServer = oAuthServer;
        }

        public Task<Token> GetReferenceTokenAsync(string handle)
        {
            
            var token=this._oAuthServer.GetByTicket(handle);

            return Task.FromResult(new Token
            {
                ClientId = token.ClientId,
                CreationTime = token.CreateTime,
                Lifetime = (int)token.Expires.TotalSeconds,
                AccessTokenType = AccessTokenType.Reference,
                Claims = new List<Claim>
                {
                     new Claim(ClaimTypes.NameIdentifier,
                               token.Owner.ToString(),
                               ClaimValueTypes.String),

                     new Claim(JwtClaimTypes.Scope,
                              token.Owner.Type== OwnerType.App? OAuthDefault.ApiScope:OAuthDefault.Scope,
                               ClaimValueTypes.String),

                     new Claim(JwtClaimTypes.Scope,
                               IdentityServerConstants.StandardScopes.OpenId),

                     new Claim(JwtClaimTypes.Subject,token.Owner.Id)
                },
                Issuer = OAuthDefault.Issuer,
            });
        }

        public Task RemoveReferenceTokenAsync(string handle)
        {
            this._oAuthServer.Remove(handle);
            return Task.FromResult(0);
        }

        public Task RemoveReferenceTokensAsync(string subjectId, string clientId)
        {
            this._oAuthServer.Remove(clientId, subjectId);
            return Task.FromResult(0);
        }

        public Task<string> StoreReferenceTokenAsync(Token token)
        {
            var owner = token.Scopes.First() == OAuthDefault.ApiScope ? new OwnerDto
            {
                Id = token.ClientId,
                Type = OwnerType.App,
            } : new OwnerDto
            {
                Id = token.SubjectId,
                Type = OwnerType.User
            };
            var o = _oAuthServer.CreateToken(owner, token.ClientId);
            return Task.FromResult(o.Ticket);
        }
    }
}
