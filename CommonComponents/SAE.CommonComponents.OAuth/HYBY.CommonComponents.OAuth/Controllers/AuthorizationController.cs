using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authentication;
using SAE.CommonLibrary.OAuth;
using System.Security.Principal;
using System.Security.Claims;

namespace SAE.CommonComponents.OAuth.Controllers
{
    [Authorize]
    public class AuthorizationController : Controller
    {


        public AuthorizationController()
        {
            
        }

        [HttpGet("~/connect/authorize")]
        public async Task<IActionResult> Authorize(CancellationToken cancellationToken)
        {
         
            // Extract the authorization request from the ASP.NET environment.
            var request = HttpContext.GetOpenIdConnectRequest();
            
            if (request == null)
            {
                return View("Error", new OpenIdConnectResponse
                {
                    Error = OpenIdConnectConstants.Errors.ServerError,
                    ErrorDescription = "An internal error has occurred."
                });
            }

            var authenticateResult = await this.HttpContext.AuthenticateAsync();
            
            return this.SignIn(authenticateResult.Principal, OAuthDefault.AuthenticationScheme);
        }
    }
}