using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using SAE.CommonComponents.OAuth.Application;
using IdentityModel;
using System.Security.Claims;

namespace SAE.CommonComponents.OAuth.Code
{
    public class ProfileService : IProfileService
    {
        private readonly IOAuthServer _oAuthServer;

        public ProfileService(IOAuthServer oAuthServer)
        {
            _oAuthServer = oAuthServer;
        }
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub=context.Subject.FindFirst(JwtClaimTypes.Subject);
            if (sub != null)
            {
                var user = this._oAuthServer.GetById(sub.Value);
                context.IssuedClaims.Add(new Claim(JwtClaimTypes.Id, user.Id));
                context.IssuedClaims.Add(new Claim(JwtClaimTypes.Name, user.Name));
            }
            

            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;

            return Task.FromResult(0);
        }
    }
}
