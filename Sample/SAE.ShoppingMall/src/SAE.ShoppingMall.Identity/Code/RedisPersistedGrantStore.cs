﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Http;
using SAE.CommonLibrary.Cache;
using SAE.CommonLibrary.Json;
using SAE.CommonLibrary.OAuth;
using SAE.ShoppingMall.Identity.Application;

namespace SAE.ShoppingMall.Identity.Code
{
    public class RedisPersistedGrantStore : IPersistedGrantStore
    {
        #region Private Readonly Field
        private readonly ICache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJsonConvertor _jsonConvertor;
        private readonly IAppService _appService;
        #endregion
        public RedisPersistedGrantStore(ICache cache,
                                        IHttpContextAccessor httpContextAccessor,
                                        IAppService appService,
                                        IJsonConvertor jsonConvertor)
        {
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
            _appService = appService;
            _jsonConvertor = jsonConvertor;
        }
        #region IPersistedGrantStore Member
        public Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            return Task.FromResult(Enumerable.Empty<PersistedGrant>());
        }

        public async Task<PersistedGrant> GetAsync(string key)
        {

            var result = await this._cache.GetAsync<PersistedGrant>(key);

            return result;
        }

        public Task RemoveAllAsync(string subjectId, string clientId)
        {
            return Task.FromResult(0);
        }

        public Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            return Task.FromResult(0);
        }

        public async Task RemoveAsync(string key)
        {
            await this._cache.RemoveAsync(key);
        }

        public async Task StoreAsync(PersistedGrant grant)
        {
            if (grant.Expiration.HasValue)
            {
                var date = grant.Expiration.Value.ToLocalTime();
                await this._cache.AddAsync(grant.Key, grant, date);
            }
            else
            {
                await this._cache.AddAsync(grant.Key, grant, TimeSpan.FromSeconds(300));
            }
            //var time = grant.Expiration.HasValue ? grant.Expiration.Value.ToLocalTime() - DateTime.Now : TimeSpan.FromSeconds(60);

            //await this._cache.AddAsync(grant.Key, grant, time);

            //var client = this._appService.Find(grant.ClientId);
            //Dictionary<string, string> dic;
            //string value;
            //if (this._httpContextAccessor
            //        .HttpContext
            //        .Request
            //        .Cookies
            //        .TryGetValue(OAuthDefault.SingoutCookie, out value))
            //{
            //    dic = _jsonConvertor.Deserialize<Dictionary<string, string>>(value);

            //}
            //else
            //{
            //    dic = new Dictionary<string, string>();
            //}

            //dic[grant.ClientId] = client.Signout;

            //value = this._jsonConvertor.Serialize(dic);

            //this._httpContextAccessor
            //    .HttpContext
            //    .Response
            //    .Cookies
            //    .Append(OAuthDefault.SingoutCookie, value, new CookieOptions
            //    {
            //        Expires = DateTime.Now.Add(TimeSpan.FromSeconds(OAuthDefault.UserTokenLifetime))
            //    });
        }
        #endregion
    }
}