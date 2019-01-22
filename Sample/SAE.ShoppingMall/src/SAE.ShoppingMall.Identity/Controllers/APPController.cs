﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SAE.CommonLibrary.MvcExtension;
using SAE.ShoppingMall.Identity.Application;
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
    }
}