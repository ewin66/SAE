using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SAE.CommonLibrary.MvcExtension;
using SAE.ShoppingMall.Identity.Application;
using SAE.ShoppingMall.Identity.Dto;
using SAE.ShoppingMall.Identity.Dto.Query;

namespace SAE.ShoppingMall.Identity.Controllers
{
    public class AppController : Controller
    {

        private readonly IAppService _appService;
        public AppController(IAppService appService)
        {
            this._appService = appService;
        }

        [RequestSeparate]
        public IActionResult Index(AppQuery query)
        {
            var data = this._appService.Paging(query);
            return this.Json(data);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Verify]
        [StandardOutput]
        public IActionResult Add(AppDto app)
        {
            this._appService.Register(app);
            return Json(app);
        }

        public IActionResult Edit(Guid id)
        {
            ViewData.Model = this._appService.GetById(id.ToString());
            return View();
        }

        [HttpPost]
        [Verify]
        [StandardOutput]
        public IActionResult Edit(AppDto app)
        {
            this._appService.Change(app);
            return Json(app);
        }
    }
}