using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.ShoppingMall.Controllers
{
    public class AccountController:Controller
    {
        public ActionResult LogOut()
        {
            return this.SignOut(OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
