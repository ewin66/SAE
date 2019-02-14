using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SAE.ShoppingMall.Identity.Controllers
{

    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interactionService;

        public HomeController(IIdentityServerInteractionService interactionService)
        {
            this._interactionService = interactionService;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Error(string errorId)
        {
            var result = await this._interactionService.GetErrorContextAsync(errorId);
            return this.Json(result);
        }
    }
}