using Microsoft.AspNetCore.Mvc;
using SAE.ShoppingMall.Identity.API.Models;
using SAE.ShoppingMall.Identity.Application;
using SAE.ShoppingMall.Identity.DocumentService;
using SAE.ShoppingMall.Identity.DocumentService.Specification;
using SAE.ShoppingMall.Identity.Dto;
using SAE.ShoppingMall.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SAE.ShoppingMall.Identity.API.Controllers
{
    [Route("api/[controller]")]
    public class AppController : Controller
    {
        private readonly IAppQueryService _appQueryService;
        private readonly IAppService _appService;
        public AppController(IAppQueryService appQueryService,IAppService appService)
        {
            this._appQueryService = appQueryService;
            this._appService = appService;
        }

        [HttpGet]
        [Route("paging")]
        public IPagingResult<AppDto> Paging(AppQueryModel appQueryModel)
        {
            AppSpecification specification = appQueryModel;
            return this._appQueryService.Paging(appQueryModel, specification);
        }

        [HttpPost]
        [Route("")]
        public void Create(AppDto appDto)
        {
            this._appService.Register(appDto);
        }

        [HttpPut]
        [Route("")]
        public void Change(AppDto appDto)
        {
            this._appService.Change(appDto);
        }

        public void Delete(string id)
        {
            this._appService
        }

        [HttpGet]
        [Route("{appId}")]
        public AppDto Find(string appId)
        {
            return this._appService.Find(appId);
        }
    }
}
