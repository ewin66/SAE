using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using SAE.ShoppingMall.Identity.Application;
using SAE.ShoppingMall.Identity.DocumentService;

namespace SAE.ShoppingMall.Identity.Code
{
    public class ProfileService : IProfileService
    {
        private readonly IUserQueryService _userQueryService;
        public ProfileService(IUserQueryService userQueryService)
        {
            this._userQueryService = userQueryService;
        }
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.FindFirst(JwtClaimTypes.Subject);
            if (sub != null)
            {
                var user= this._userQueryService.Find(sub.Value);
                context.IssuedClaims.Add(new Claim(JwtClaimTypes.Id, user.Id));
                context.IssuedClaims.Add(new Claim(JwtClaimTypes.Name, user.Name));
            }
            
            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
#warning 总是处于激活状态，后期可以更改
            return Task.FromResult(0);
        }
    }
}
