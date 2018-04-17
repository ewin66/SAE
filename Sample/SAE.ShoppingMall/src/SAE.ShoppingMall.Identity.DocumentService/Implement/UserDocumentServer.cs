using System;
using System.Collections.Generic;
using SAE.CommonLibrary.Storage;
using SAE.ShoppingMall.Identity.Dto;
using SAE.ShoppingMall.Infrastructure;
using SAE.ShoppingMall.Infrastructure.Specification;
using System.Linq;
using SAE.ShoppingMall.DocumentService;

namespace SAE.ShoppingMall.Identity.DocumentService.Implement
{
    public class UserDocumentServer :DocumentServer<UserDto>,IUserQueryServer
    {
        public UserDocumentServer(IStorage storage) : base(storage)
        {
        }

        public UserDto Find(string loginName)
        {
            return this.Storage.AsQueryable<UserDto>()
                               .FirstOrDefault(s => s.Credentials.Name == loginName);
        }

    }
}
