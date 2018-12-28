using SAE.CommonLibrary.EventStore;
using SAE.CommonLibrary.EventStore.Document;
using SAE.CommonLibrary.Storage;
using SAE.ShoppingMall.Identity.Domain;
using SAE.ShoppingMall.Identity.Domain.ValueObject;
using SAE.ShoppingMall.Identity.Dto;
using SAE.ShoppingMall.Infrastructure;
using SAE.CommonLibrary.Common;

namespace SAE.ShoppingMall.Identity.Application.Implement
{
    public class AppService: ApplicationService,IAppService
    {
        public AppService(IDocumentStore documentStore, IStorage storage) : base(documentStore, storage)
        {
        }

        public void Change(AppDto appDto)
        {
            var app= this._documentStore.Find<App>(IdentityGenerator.Build(appDto.AppId));
            var newApp = appDto.To<App>();
            app.Change(newApp);
            this._documentStore.Save(app);
        }

        public AppDto Find(string appId)
        {
            var app = this._documentStore.Find<App>(IdentityGenerator.Build(appId));
            return app.To<AppDto>();
        }

        public AppDto Register(AppDto appDto)
        {
            var app = new App(appDto.Name,
                              new ClientCredentials(appDto.AppId, appDto.AppSecret),
                              new SignEndpoint(appDto.Signin, appDto.Signout));
            this._documentStore.Save(app);
            return app.To<AppDto>();
        }

        public void Remove(string appId)
        {
            var app = this._documentStore.Find<App>(IdentityGenerator.Build(appId));
            app.ChangeStatus(Status.Delete);
            this._documentStore.Save(app);
        }
    }
}
