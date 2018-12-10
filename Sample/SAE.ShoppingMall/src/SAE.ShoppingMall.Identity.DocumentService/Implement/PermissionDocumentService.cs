using SAE.CommonLibrary.Storage;
using SAE.ShoppingMall.DocumentService;
using SAE.ShoppingMall.Identity.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SAE.ShoppingMall.Identity.DocumentService.Implement
{
    public class PermissionDocumentService : DocumentService<PermissionDto>, IPermissionQueryService
    {
        public PermissionDocumentService(IStorage storage) : base(storage)
        {
        }

        public PermissionDto GetById(string id)
        {
            return this.Storage.AsQueryable<PermissionDto>()
                               .FirstOrDefault(s => s.Id == id);
        }
    }
}
