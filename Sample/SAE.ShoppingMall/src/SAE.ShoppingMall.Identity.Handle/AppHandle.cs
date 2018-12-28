using SAE.CommonLibrary.EventStore;
using SAE.CommonLibrary.MQ;
using SAE.ShoppingMall.Identity.Domain.Event;
using SAE.ShoppingMall.Identity.Dto;

namespace SAE.ShoppingMall.Identity.Handle
{
    public class AppHandle : IHandler<RegisterAppEvent>,
                             IHandler<ChangeAppEndpointEvent>,
                             IHandler<ChangeAppNameEvent>,
                             IHandler<ChangeAppSecretEvent>,
                             IHandler<ChangeAppStatusEvent>
    {
        private readonly IAppQueryService _appQueryService;
        private readonly IDocumentService<AppDto> _appDocumentService;
        public AppHandle(IAppQueryService appQueryService, IDocumentService<AppDto> appDocumentService)
        {
            this._appQueryService = appQueryService;
            this._appDocumentService = appDocumentService;
        }

        public void Handle(RegisterAppEvent @event)
        {
            this._appDocumentService.Add(new AppDto
            {
                AppId = @event.Id,
                AppSecret = @event.Secret,
                CreateTime = @event.CreateTime,
                Name = @event.Name,
                Signin = @event.Signin,
                Signout = @event.Signout,
                Status = (int)@event.Status
            });
        }

        public void Handle(ChangeAppEndpointEvent @event)
        {
            var app= this._appQueryService.GetById((@event as IEvent).Id);
            app.Signin = @event.Signin;
            app.Signout = @event.Signout;
            this._appDocumentService.Update(app);
        }

        public void Handle(ChangeAppNameEvent @event)
        {
            var app = this._appQueryService.GetById((@event as IEvent).Id);
            app.Name = @event.Name;
            
            this._appDocumentService.Update(app);
        }

        public void Handle(ChangeAppSecretEvent @event)
        {
            var app = this._appQueryService.GetById((@event as IEvent).Id);
            app.AppSecret = @event.Secret;
            this._appDocumentService.Update(app);
        }

        public void Handle(ChangeAppStatusEvent @event)
        {
            var app = this._appQueryService.GetById((@event as IEvent).Id);
            app.Status = (int)@event.Status;
            this._appDocumentService.Update(app);
        }
    }
}
