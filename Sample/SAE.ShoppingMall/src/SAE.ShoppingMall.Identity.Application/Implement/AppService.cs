using SAE.CommonLibrary.EventStore;
using SAE.CommonLibrary.EventStore.Document;
using SAE.CommonLibrary.Storage;
using SAE.ShoppingMall.Identity.Domain;
using SAE.ShoppingMall.Identity.Domain.ValueObject;
using SAE.ShoppingMall.Identity.Dto;
using SAE.ShoppingMall.Infrastructure;
using SAE.CommonLibrary.Common;
using SAE.ShoppingMall.Identity.Dto.Query;
using System.Linq;
using SAE.ShoppingMall.Infrastructure.Specification;

namespace SAE.ShoppingMall.Identity.Application.Implement
{
    public class AppService: ApplicationService,IAppService
    {
        public AppService(IDocumentStore documentStore, IStorage storage) : base(documentStore, storage)
        {
        }

        public void Change(AppDto appDto)
        {
            this.Update<AppDto, App>(appDto);
        }

        public AppDto GetById(string appId)
        {
            var app = this._documentStore.Find<App>(IdentityGenerator.Build(appId));
            return app.To<AppDto>();
        }

        public IPagingResult<AppDto> Paging(AppQuery query)
        {
            return this.Paging(query, query);
        }

        public void Register(AppDto appDto)
        {
            var app = new App(appDto.Name,
                              new ClientCredentials(appDto.AppId,appDto.AppSecret),
                              new SignEndpoint(appDto.Signin, appDto.Signout));
            this._documentStore.Save(app);
        }

        public void Remove(string appId)
        {
            this.Remove<App>(appId);
        }
    }
}
