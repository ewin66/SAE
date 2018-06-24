using System;
using System.Collections.Generic;
using System.Text;
using Nelibur.ObjectMapper;
using SAE.CommonLibrary.EventStore;
using SAE.CommonLibrary.EventStore.Document;
using SAE.ShoppingMall.Identity.DocumentService;
using SAE.ShoppingMall.Identity.Domain;
using SAE.ShoppingMall.Identity.Domain.ValueObject;
using SAE.ShoppingMall.Identity.Dto;
using SAE.ShoppingMall.Infrastructure;

namespace SAE.ShoppingMall.Identity.Application.Implement
{
    public class AppService: ApplicationService,IAppService
    {
        private readonly IAppQueryService _appQueryService;
        public AppService(IDocumentStore documentStore,IAppQueryService appQueryServer):base(documentStore)
        {
            this._appQueryService = appQueryServer;
        }


        public void Change(AppDto appDto)
        {
            var app= this._documentStore.Find<App>(IdentityGenerator.Build(appDto.AppId));
            var newApp = Utils.Map<App>(appDto);
            app.Change(newApp);
            this._documentStore.Save(app);
        }

        public AppDto Find(string appId)
        {
            var app = this._documentStore.Find<App>(IdentityGenerator.Build(appId));
            return Utils.Map<AppDto>(app);
        }

        public AppDto Register(AppDto appDto)
        {
            var app = new App(appDto.Name,
                              new ClientCredentials(appDto.AppId, appDto.AppSecret),
                              new SignEndpoint(appDto.Signin, appDto.Signout));
            this._documentStore.Save(app);
            return Utils.Map<AppDto>(app);
        }

        public void Remove(string appId)
        {
            var app = this._documentStore.Find<App>(IdentityGenerator.Build(appId));
            app.ChangeStatus(Status.Delete);
            this._documentStore.Save(app);
        }
    }
}
