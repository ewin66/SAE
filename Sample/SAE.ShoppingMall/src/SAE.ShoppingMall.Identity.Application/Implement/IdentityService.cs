using SAE.CommonLibrary.Common;
using SAE.CommonLibrary.Common.Check;
using SAE.CommonLibrary.EventStore;
using SAE.CommonLibrary.EventStore.Document;
using SAE.CommonLibrary.Storage;
using SAE.ShoppingMall.Identity.Domain;
using SAE.ShoppingMall.Identity.Domain.ValueObject;
using SAE.ShoppingMall.Identity.Dto;
using SAE.ShoppingMall.Identity.Dto.Query;
using SAE.ShoppingMall.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAE.ShoppingMall.Identity.Application.Implement
{
    public class IdentityService : ApplicationService,IIdentityService
    {
        
        public IdentityService(IDocumentStore documentStore,IStorage storage):base(documentStore, storage)
        {
            
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

        public UserDto GetById(string id)
        {
            var user = this._documentStore.Find<User>(id.ToIdentity());
            return user.To<UserDto>();
        }

        public UserDto GetByLoginName(string loginName)
        {
            var userDto = this._storage.AsQueryable<UserDto>()
                                       .FirstOrDefault(s => s.Credentials.Name == loginName);
            return userDto;
        }

        public void GrantRolePermissions(string roleId, IEnumerable<string> permissions)
        {
            var role = this._documentStore.Find<Role>(roleId.ToIdentity());
            Assert.Build(role).NotNull();
            role.AddRange(permissions);
            this._documentStore.Save(role);
        }

        public void GrantUserRoles(string userId, IEnumerable<string> roles)
        {
            var user = this._documentStore.Find<User>(userId.ToIdentity());
            user.GrantRole(roles);
            this._documentStore.Save(user);
        }

        public UserDto Authentication(CredentialsDto credentialsDto)
        {
            var userDto = this._storage.AsQueryable<UserDto>()
                                       .FirstOrDefault(s => s.Credentials.Name == credentialsDto.Name);

            Assert.Build(userDto)
                  .NotNull($"用户\"{credentialsDto.Name}\"不存在");

            var user = this._documentStore.Find<User>(userDto.Id.ToIdentity());

            Assert.Build(user.VerifyPassword(credentialsDto.Password))
                  .True("密码不正确");

            return user.To<UserDto>();
        }


        public void Create(CredentialsDto credentialsDto)
        {
            var user = new User(new Credentials(credentialsDto.Name, credentialsDto.Password));
            this._documentStore.Save(user);
        }

        public void RemoveUser(string id)
        {
            throw new NotImplementedException();
            var user = this._documentStore.Find<User>(id.ToIdentity());
            user.Destory();
            this._documentStore.Save(user);
        }

        public void Update(UserDto dto)
        {
            this.Update<UserDto, User>(dto);
        }

        public void Update(RoleDto dto)
        {
            this.Update<RoleDto, Role>(dto);
        }

        public void RemoveRole(string roleId)
        {
            this.Remove<Role>(roleId);
        }

        public void Update(PermissionDto dto)
        {
            this.Update<PermissionDto, Permission>(dto);
        }

        public void RemovePermission(string permissionId)
        {
            this.Remove<Permission>(permissionId);
        }

        public  IPagingResult<UserDto> Paging(UserQuery query)
        {
            return this.Paging(query, query);
        }
    }
}
