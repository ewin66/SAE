using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAE.CommonLibrary.Storage;
using SAE.ShoppingMall.DocumentService;
using SAE.ShoppingMall.Identity.Dto;

namespace SAE.ShoppingMall.Identity.DocumentService.Implement
{
    public class AppDocuemntService : DocumentService<AppDto>, IAppQueryService
    {
        public AppDocuemntService(IStorage storage) : base(storage)
        {
        }

        public AppDto GetById(string appId)
        {
            return this.Storage.AsQueryable<AppDto>()
                               .FirstOrDefault(s => s.AppId == appId);
        }
    }
}
