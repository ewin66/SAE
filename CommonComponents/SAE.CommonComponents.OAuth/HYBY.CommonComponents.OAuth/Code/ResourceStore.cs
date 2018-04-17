using SAE.CommonComponents.OAuth.Application;
using SAE.CommonComponents.OAuth.Application.Dtos;
using SAE.CommonLibrary.OAuth;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.CommonComponents.OAuth.Code
{
    internal class ResourceStore : IResourceStore
    {
        #region Private Member

        private readonly IOAuthServer _oAuthServer;
        private readonly IEnumerable<IdentityResource> _identityResource;
        private readonly IEnumerable<ApiResource> _apiResource;

        #endregion

        #region Ctor
        public ResourceStore(IOAuthServer oAuthServer)
        {
            _oAuthServer = oAuthServer;
            _identityResource = new List<IdentityResource>()
            {
               new IdentityResources.OpenId(),
               new IdentityResources.Profile(),
            };
            _apiResource = Enumerable.Empty<ApiResource>();
        }
        #endregion

        #region IResourceStore Member
        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            var certification = _oAuthServer.GetByClientId(name);

            return Task.FromResult(this.ConvertToApiResource(certification));
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return Task.FromResult(new List<ApiResource> {
                   new ApiResource(OAuthDefault.ApiScope)
                   }.AsEnumerable());
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return Task.FromResult(this._identityResource);
        }


        public Task<Resources> GetAllResourcesAsync()
        {
            return Task.FromResult(new Resources(_identityResource, _apiResource));
        }
        #endregion

        #region Private Mehtod
        private ApiResource ConvertToApiResource(AppDto dto)
        {
            if (dto == null)
                return null;
            return new ApiResource(dto.Client.Id, dto.Name)
            {
                Scopes = new List<Scope> { new Scope(OAuthDefault.ApiScope) },
                ApiSecrets = new List<Secret>
                {
                    new Secret(dto.Client.Secret.Sha512())
                }
            };
        } 
        #endregion

    }
}
