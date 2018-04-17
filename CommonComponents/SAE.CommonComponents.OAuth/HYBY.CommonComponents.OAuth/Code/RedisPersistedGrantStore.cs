using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Http;
using SAE.CommonLibrary.Json;
using SAE.CommonComponents.OAuth.Infrastructure;
using SAE.CommonLibrary.Cache;
using SAE.CommonComponents.OAuth.Application;
using SAE.CommonLibrary.OAuth;

namespace SAE.CommonComponents.OAuth.Code
{
    internal class RedisPersistedGrantStore : IPersistedGrantStore
    {

        #region Private Readonly Field
        private readonly ICache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOAuthServer _oAuthServer;
        private readonly IJsonConvertor _jsonConvertor;
        #endregion

        #region Ctor
        public RedisPersistedGrantStore(ICache cache,
                                        IHttpContextAccessor httpContextAccessor,
                                        IOAuthServer oAuthServer,
                                        IJsonConvertor jsonConvertor)
        {
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
            _oAuthServer = oAuthServer;
            _jsonConvertor = jsonConvertor;
        }
        #endregion

        #region IPersistedGrantStore Member
        public Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            return Task.FromResult(Enumerable.Empty<PersistedGrant>());
        }

        public async Task<PersistedGrant> GetAsync(string key)
        {

            var result = await this._cache.GetAsync<PersistedGrant>(key);

            await this._cache.RemoveAsync(key);

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
            var time = grant.Expiration.HasValue ? grant.Expiration - DateTime.Now: null;

            await this._cache.AddAsync(grant.Key, grant, time);

            var client=_oAuthServer.GetByClientId(grant.ClientId);
            Dictionary<string, string> dic;
            string value;
            if (this._httpContextAccessor
                    .HttpContext
                    .Request
                    .Cookies
                    .TryGetValue(OAuthDefault.SingoutCookie, out value))
            {
                dic = _jsonConvertor.Deserialize<Dictionary<string, string>>(value);
               
            }
            else
            {
                dic = new Dictionary<string, string>();
            }

            dic[grant.ClientId] = client.Signout;

            value = this._jsonConvertor.SerializeLowerCase(dic);

            this._httpContextAccessor
                .HttpContext
                .Response
                .Cookies.
                Append(OAuthDefault.SingoutCookie, value, new CookieOptions
                {
                    Expires =DateTime.Now.Add(Domain.AggregateRoot.Token.UserTokenLifetime)
                });
        } 
        #endregion
    }
}
