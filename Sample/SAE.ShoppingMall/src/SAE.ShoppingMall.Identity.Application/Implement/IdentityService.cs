using System;
using System.Collections.Generic;
using Nelibur.ObjectMapper;
using SAE.CommonLibrary.Common.Check;
using SAE.CommonLibrary.EventStore;
using SAE.CommonLibrary.EventStore.Document;
using SAE.CommonLibrary.EventStore.Identity;
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

        public bool Authorization(string userId, string flag)
        {
            var user = this._documentStore.Find<User>(userId.ToIdentity());
            return user.Authorize(flag, this._documentStore.Find<Role>, this._documentStore.Find<Permission>);
        }

        public void Create(RoleDto dto)
        {
            var role= new Role(dto.Name);
            this._documentStore.Save(role);
        }

        public void Create(PermissionDto dto)
        {
            var permission= new Permission(dto.Name);
            this._documentStore.Save(permission);
        }

        public UserDto Find(string id)
        {
            var user = this._documentStore.Find<User>(id.ToIdentity());
            return new UserDto();
        }

        public void GrantRolePermissions(string roleId, IEnumerable<string> permissions)
        {
            throw new NotImplementedException();
        }

        public void GrantUserRoles(string userId, IEnumerable<string> roles)
        {
            throw new NotImplementedException();
        }

        public UserDto Login(CredentialsDto credentialsDto)
        {
            var userDto = this._userQueryService.Find(credentialsDto.Name);

            Assert.Build(userDto)
                  .NotNull($"用户\"{credentialsDto.Name}\"不存在");

            var user = this._documentStore.Find<User>(userDto.Id.ToIdentity());

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
