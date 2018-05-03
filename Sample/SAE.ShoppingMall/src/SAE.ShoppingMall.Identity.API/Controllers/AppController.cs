using Microsoft.AspNetCore.Mvc;
using SAE.ShoppingMall.Identity.API.Models;
using SAE.ShoppingMall.Identity.DocumentService;
using SAE.ShoppingMall.Identity.Dto;
using SAE.ShoppingMall.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SAE.ShoppingMall.Identity.API.Controllers
{
    [Route("api/[controller]")]
    public class AppController : Controller
    {
        private readonly IAppQueryService _appQueryService;
        public AppController(IAppQueryService appQueryService)
        {
            this._appQueryService = appQueryService;
        }

        [HttpGet]
        [Route("paging")]
        public IPagingResult<AppDto> Paging(AppQueryModel appQueryModel)
        {
            return this._appQueryService.Paging(appQueryModel,null);
        }
    }
}
