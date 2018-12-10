using SAE.CommonLibrary.EventStore;
using SAE.CommonLibrary.MQ;
using SAE.ShoppingMall.DocumentService;
using SAE.ShoppingMall.Identity.DocumentService;
using SAE.ShoppingMall.Identity.Domain.Event;
using SAE.ShoppingMall.Identity.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.ShoppingMall.Identity.Handle
{
    public class RoleHandle : IHandler<ChangeRoleNameEvent>, IHandler<CreateRoleEvent>
    {
        private readonly IRoleQueryService _roleQueryService;
        private readonly IDocumentService<RoleDto> _roleDocumentServer;
        public RoleHandle(IRoleQueryService roleQueryService,IDocumentService<RoleDto> documentService)
        {
            this._roleQueryService = roleQueryService;
            this._roleDocumentServer = documentService;
        }
        public void Handle(CreateRoleEvent @event)
        {
            var dto = new RoleDto()
            {
                Id = @event.Id,
                Name = @event.Name,
                CreateTime = @event.CreateTime
            };
            this._roleDocumentServer.Add(dto);
        }

        public void Handle(ChangeRoleNameEvent @event)
        {
            var dto = this._roleQueryService.GetById((@event as IEvent).Id);
            dto.Name = @event.Name;
            this._roleDocumentServer.Update(dto);
        }
    }
}
