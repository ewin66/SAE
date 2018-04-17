using System;
using Nelibur.ObjectMapper;
using SAE.CommonLibrary.Common.Check;
using SAE.CommonLibrary.EventStore.Document;
using SAE.ShoppingMall.Identity.DocumentService;
using SAE.ShoppingMall.Identity.Domain;
using SAE.ShoppingMall.Identity.Dto;
using SAE.ShoppingMall.Infrastructure;

namespace SAE.ShoppingMall.Identity.Application.Implement
{
    public class IdentityService : IIdentityService
    {
        
        private readonly IDocumentStore _documentStore;
        private readonly IUserQueryServer _userQueryService;
        static IdentityService()
        {
            TinyMapper.Bind<User, UserDto>();
        }
        public IdentityService(IDocumentStore documentStore,IUserQueryServer userQueryService)
        {
            this._documentStore = documentStore;
            this._userQueryService = userQueryService;
        }

        public UserDto Find(Guid id)
        {
            var user = this._documentStore.Find<User>(new CommonLibrary.EventStore.Identity(id.ToString()));
            return new UserDto();
        }

        public UserDto Login(CredentialsDto credentialsDto)
        {
            var userDto = this._userQueryService.Find(credentialsDto.Name);
            Assert.Build(userDto)
                  .NotNull($"用户\"{credentialsDto.Name}\"不存在,或密码不正常");
            var user = this._documentStore.Find<User>(new SAE.CommonLibrary.EventStore.Identity(userDto.Id));
            return userDto;
        }

        UserDto IIdentityService.Register(CredentialsDto credentialsDto)
        {
            var user = new User();
            user.Create(new Domain.ValueObject.Credentials(credentialsDto.Name, credentialsDto.Password));
            this._documentStore.Save(user);
            return Utils.Map<UserDto>(user);
        }
    }
}
