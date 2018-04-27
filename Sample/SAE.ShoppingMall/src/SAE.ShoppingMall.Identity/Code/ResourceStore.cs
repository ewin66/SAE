using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using SAE.CommonLibrary.OAuth;
using SAE.ShoppingMall.Identity.Application;
using SAE.ShoppingMall.Identity.Dto;

namespace SAE.ShoppingMall.Identity.Code
{
    public class ResourceStore : IResourceStore
    {
        #region Private Member

        private readonly IAppService _appService;
        private readonly IEnumerable<IdentityResource> _identityResource;
        private readonly IEnumerable<ApiResource> _apiResource;

        #endregion

        #region Ctor
        public ResourceStore(IAppService appService)
        {
            _appService = appService;
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
            var certification = this._appService.Find(name);

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
            return new ApiResource(dto.AppId, dto.Name)
            {
                Scopes = new List<Scope> { new Scope(OAuthDefault.ApiScope) },
                ApiSecrets = new List<Secret>
                {
                    new Secret(dto.AppSecret.Sha512())
                }
            };
        }
        #endregion
    }
}
