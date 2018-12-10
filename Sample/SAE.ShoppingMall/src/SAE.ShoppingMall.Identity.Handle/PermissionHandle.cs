using SAE.CommonLibrary.EventStore;
using SAE.CommonLibrary.MQ;
using SAE.ShoppingMall.DocumentService;
using SAE.ShoppingMall.Identity.DocumentService;
using SAE.ShoppingMall.Identity.Domain.Event;
using SAE.ShoppingMall.Identity.Dto;

namespace SAE.ShoppingMall.Identity.Handle
{
    public class PermissionHandle : IHandler<CreatePermissionEvent>, IHandler<ChangePermissionEvent>
    {
        private readonly IPermissionQueryService _permissionQueryService;
        private readonly IDocumentService<PermissionDto> _permissionDocumentService;
        public PermissionHandle()
        {

        }
        public void Handle(ChangePermissionEvent @event)
        {
            var permissionDto = this._permissionQueryService.GetById((@event as IEvent).Id);
            permissionDto.Name = @event.Name;
            permissionDto.Pattern = @event.Pattern;
            this._permissionDocumentService.Update(permissionDto);
        }

        public void Handle(CreatePermissionEvent @event)
        {
            var permissionDto= new PermissionDto
            {
                Id = @event.Id,
                Name = @event.Name,
                Pattern = @event.Pattern
            };
            this._permissionDocumentService.Add(permissionDto);
        }
    }
}
