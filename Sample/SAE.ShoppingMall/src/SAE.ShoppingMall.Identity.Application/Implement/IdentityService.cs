using System;
using Nelibur.ObjectMapper;
using SAE.CommonLibrary.Common.Check;
using SAE.CommonLibrary.EventStore;
using SAE.CommonLibrary.EventStore.Document;
using SAE.ShoppingMall.Identity.DocumentService;
using SAE.ShoppingMall.Identity.Domain;
using SAE.ShoppingMall.Identity.Domain.ValueObject;
using SAE.ShoppingMall.Identity.Dto;
using SAE.ShoppingMall.Infrastructure;

namespace SAE.ShoppingMall.Identity.Application.Implement
{
    public class IdentityService : ApplicationService,IIdentityService
    {
        
        private readonly IUserQueryService _userQueryService;
        public IdentityService(IDocumentStore documentStore,IUserQueryService userQueryService):base(documentStore)
        {
            this._userQueryService = userQueryService;
        }

        public UserDto Find(string id)
        {
            var user = this._documentStore.Find<User>(IdentityGenerator.Build(id));
            return new UserDto();
        }

        public UserDto Login(CredentialsDto credentialsDto)
        {
            var userDto = this._userQueryService.Find(credentialsDto.Name);

            Assert.Build(userDto)
                  .NotNull($"用户\"{credentialsDto.Name}\"不存在");

            var user = this._documentStore.Find<User>(IdentityGenerator.Build(userDto.Id));

            user.VerifyPassword(credentialsDto.Password);

            return userDto;
        }


        UserDto IIdentityService.Register(CredentialsDto credentialsDto)
        {
            var user = new User(new Credentials(credentialsDto.Name, credentialsDto.Password));
            this._documentStore.Save(user);
            return Utils.Map<UserDto>(user);
        }
    }
}
