using SAE.CommonLibrary.Storage;
using SAE.ShoppingMall.DocumentService;
using SAE.ShoppingMall.Identity.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SAE.ShoppingMall.Identity.DocumentService.Implement
{

    public class RoleDocumentService : DocumentService<RoleDto>, IRoleQueryService
    {
        public RoleDocumentService(IStorage storage) : base(storage)
        {
        }

        public RoleDto GetById(string id)
        {
           return this.Storage.AsQueryable<RoleDto>()
                              .FirstOrDefault(r => r.Id == id);
        }

        public RoleDto GetByName(string Name)
        {
            return this.Storage.AsQueryable<RoleDto>()
                              .FirstOrDefault(r => r.Name == Name);
        }
    }
}
