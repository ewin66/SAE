using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SAE.CommonLibrary.Common;
using SAE.CommonLibrary.MvcExtension;
using SAE.ShoppingMall.Identity.Application;
using SAE.ShoppingMall.Identity.Dto;
using SAE.ShoppingMall.Identity.Dto.Query;

namespace SAE.ShoppingMall.Identity.Controllers
{
    public class UserController : Controller
    {
        private readonly IIdentityService _identityService;
        public UserController(IIdentityService identityService)
        {
            this._identityService = identityService;
        }
        [RequestSeparate]
        public IActionResult Index(UserQuery query)
        {
            var model = this._identityService.Paging(query);
            return this.Json(model);
        }

        public IActionResult Edit(string id)
        {
            ViewData.Model = this._identityService.GetById(id);
            return View();
        }

        [HttpPost]
        public IActionResult Edit(UserDto dto)
        {
            this._identityService.Update(dto);
            return this.Json(new StandardResult());
        }
        
    }
}