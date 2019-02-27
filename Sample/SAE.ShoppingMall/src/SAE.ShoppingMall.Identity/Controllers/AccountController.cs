using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SAE.CommonLibrary.Common;
using SAE.CommonLibrary.EventStore;
using SAE.CommonLibrary.MvcExtension;
using SAE.ShoppingMall.Identity.Application;
using SAE.ShoppingMall.Identity.Dto;
using SAE.ShoppingMall.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SAE.ShoppingMall.Identity.Controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly IIdentityService _identityService;
        public AccountController(IIdentityService identityService,
            IIdentityServerInteractionService interactionService)
        {
            this._identityService = identityService;
            this._interactionService = interactionService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [Verify]
        [StandardOutput]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> LogIn(CredentialsDto credentials)
        {
            var user = this._identityService.Authentication(credentials);
            var result = new StandardResult();
            if (user == null)
            {
                result.StatusCode = SAE.CommonLibrary.Common.StatusCode.AccountOrPassword;
            }
            else
            {
                var claims = new List<Claim>()
                {
                   new Claim("name",user.Information.Name),
                   new Claim(ClaimTypes.Sid,user.Id),
                   new Claim("sub",user.Id),
                   new Claim("role","admin")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await this.HttpContext.SignInAsync(principal);
            }
            return this.Json(result);
        }

        
        [HttpGet()]
        public async Task<IActionResult> LogOut(string logoutId)
        {
            string redirectUrl = string.Empty;
            if (!string.IsNullOrWhiteSpace(logoutId))
            {
                var context = await this._interactionService.GetLogoutContextAsync(logoutId);
                redirectUrl = context.PostLogoutRedirectUri ?? context.SignOutIFrameUrl;
            }
            else
            {
                redirectUrl = "/";
            }

            await this.HttpContext.SignOutAsync();

            return this.Redirect(redirectUrl);
        }


        public ActionResult Register()
        {
            return View();
        }

        [Verify]
        [StandardOutput]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegisterModel model)
        {
            this._identityService.Create(model);
            var result = new StandardResult();
            return this.Json(result);
        }
    }
}
