using System;
using Microsoft.AspNetCore.Mvc;
using SAE.ShoppingMall.Identity.API.Models;
using SAE.ShoppingMall.Identity.Application;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SAE.ShoppingMall.Identity.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IIdentityService _identityService;
        public UserController(IIdentityService identityService)
        {
            this._identityService = identityService;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post(RegisterViewModel model)
        {
            this._identityService.Register(model);
        }

        [HttpGet("{id}")]
        public object Get(Guid id)
        {
            return this._identityService.Find(id);
        }

    }
}
