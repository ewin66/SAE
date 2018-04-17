using SAE.CommonComponents.OAuth.Application;
using SAE.CommonComponents.OAuth.Models;
using SAE.CommonLibrary.Json;
using SAE.CommonLibrary.OAuth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SAE.CommonComponents.OAuth.Controllers
{
    public class AccountController : Controller
    {
        private readonly IOAuthServer _oAuthServer;
        private readonly IJsonConvertor _jsonConvertor;
        public AccountController( IOAuthServer oAuthServer,IJsonConvertor jsonConvertor)
        {
            this._oAuthServer = oAuthServer;
            this._jsonConvertor = jsonConvertor;
        }


        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = "/")
        {
            var authenticateResult = await this.HttpContext.AuthenticateAsync();
            if (authenticateResult.Succeeded)
            {
                //await HttpContext.SignInAsync(OAuthDefault.AuthenticationScheme, user.Principal);
                return this.Redirect(returnUrl);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel login)
        {
            this.HttpContext.Response.Cookies.Delete(OAuthDefault.SingoutCookie);
            if (ModelState.IsValid)
            {

                var user = _oAuthServer.Login(login);

                if (user == null)
                {
                    ModelState.AddModelError(nameof(login.Name), "用户名或密码错误!");
                    return View();
                }
                var propertues = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(1),
                };

                await HttpContext.SignInAsync(user.Login.Name, user.Name, propertues);
                
                return this.Redirect(login.ReturnUrl);
            }

            return View();
        }

       
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            this.HttpContext.Response.Cookies.Delete(OAuthDefault.SingoutCookie);

            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            string value;

            if(HttpContext.Request.Cookies.TryGetValue(OAuthDefault.SingoutCookie,out value))
            {
                dictionary = _jsonConvertor.Deserialize<Dictionary<string, string>>(value);
            }
            await this.HttpContext.SignOutAsync();

            return View(model:dictionary);
        }
    }
}