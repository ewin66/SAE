using SAE.CommonLibrary.Common;
using SAE.CommonLibrary.EventStore;
using SAE.CommonLibrary.EventStore.Document;
using SAE.CommonLibrary.ObjectMapper;
using SAE.CommonLibrary.Storage;
using SAE.ShoppingMall.Identity.Domain;
using SAE.ShoppingMall.Identity.Domain.Event;
using SAE.ShoppingMall.Identity.Domain.ValueObject;
using SAE.ShoppingMall.Identity.Dto;
using SAE.ShoppingMall.Infrastructure;
using SAE.ShoppingMall.Infrastructure.Specification;
using System;
using System.Linq;

namespace SAE.ShoppingMall.Identity.Application.Implement
{
    public abstract class ApplicationService
    {
        protected readonly IDocumentStore _documentStore;
        protected readonly IStorage _storage;
        public ApplicationService(IDocumentStore documentStore, IStorage storage)
        {
            this._documentStore = documentStore;
            this._storage = storage;
        }
        static ApplicationService()
        {
            TinyMapper.Bind<UserDto, User>(config =>
            {
                config.Bind(s => s.Information.Email, t => t.Information.Contact.Email);
                config.Bind(s => s.Information.Phone, t => t.Information.Contact.Phone);
                config.Bind(s => s.Information.QQ, t => t.Information.Contact.QQ);
            }).Reverse();
            TinyMapper.Bind<UserRegisterEvent, UserDto>(config =>
            {
                config.Bind(s => s.CreateTime, t => t.CreateTime);
                config.Bind(s => s.Id, t => t.Id);
                config.Bind(s => s.LoginName, t => t.Credentials.Name);
                config.Bind(s => s.Password, t => t.Credentials.Password);
                config.Bind(s => s.Status, t => t.Status);
            });

            TinyMapper.Bind<UserRegisterEvent, User>(config =>
            {
                config.Bind(s => s.Id, t => t.Id);
                config.Bind(s => s.LoginName, t => t.Credentials.Name);
                config.Bind(s => s.Password, t => t.Credentials.Password);
                config.Bind(s => s.Salt, t => t.Credentials.Salt);
                config.Bind(s => s.Status, t => t.Status);
                config.Bind(s => s.CreateTime, t => t.CreateTime);
            });

            TinyMapper.Bind<UserChangeInfoEvent, User>(config =>
            {
                config.Bind(s => s.BirthDate, t => t.Information.BirthDate);
                config.Bind(s => s.Email, t => t.Information.Contact.Email);
                config.Bind(s => s.Hometown, t => t.Information.Hometown);
                config.Bind(s => s.Phone, t => t.Information.Contact.Phone);
                config.Bind(s => s.QQ, t => t.Information.Contact.QQ);
                config.Bind(s => s.Sex, t => t.Information.Sex);
                config.Bind(s => s.Name, t => t.Information.Name);
                config.Bind(s => s.Icon, t => t.Information.Icon);
            });

            TinyMapper.Bind<User, UserChangeInfoEvent>(config =>
            {
                config.Bind(s => s.Information.BirthDate, t => t.BirthDate);
                config.Bind(s => s.Information.Contact.Email, t => t.Email);
                config.Bind(s => s.Information.Hometown, t => t.Hometown);
                config.Bind(s => s.Information.Contact.Phone, t => t.Phone);
                config.Bind(s => s.Information.Contact.QQ, t => t.QQ);
                config.Bind(s => s.Information.Sex, t => t.Sex);
                config.Bind(s => s.Information.Name, t => t.Name);
            });

            TinyMapper.Bind<UserInfo, UserChangeInfoEvent>(config =>
             {
                 config.Bind(s => s.Contact.Email, t => t.Email);
                 config.Bind(s => s.Contact.Phone, t => t.Phone);
                 config.Bind(s => s.Contact.QQ, t => t.QQ);
             });

            TinyMapper.Bind<AppDto, App>(config =>
            {
                config.Bind(s => s.AppId, t => t.Client.Id);
                config.Bind(s => s.AppSecret, t => t.Client.Secret);
                config.Bind(s => s.Signin, t => t.Endpoint.Signin);
                config.Bind(s => s.Signout, t => t.Endpoint.Signout);
            }).Reverse();

            TinyMapper.Bind<AppCreateEvent, AppDto>(config =>
            {
                config.Bind(s => s.Id, t => t.AppId);
                config.Bind(s => s.Secret, t => t.AppSecret);
                config.Bind(s => s.Signin, t => t.Signin);
                config.Bind(s => s.Signout, t => t.Signout);
            });


            TinyMapper.Bind<AppCreateEvent, App>(config =>
            {
                config.Bind(s => s.Id, t => t.Client.Id);
                config.Bind(s => s.Secret, t => t.Client.Secret);
                config.Bind(s => s.Signin, t => t.Endpoint.Signin);
                config.Bind(s => s.Signout, t => t.Endpoint.Signout);
            });

            TinyMapper.Bind<AppChangeSecretEvent, AppDto>(config =>
            {
                config.Bind(s => s.Secret, t => t.AppSecret);
            });

            TinyMapper.Bind<AppChangeSecretEvent, App>(config =>
            {
                config.Bind(s => s.Secret, t => t.Client.Secret);
            });

            TinyMapper.Bind<AppChangeEndpointEvent, App>(config =>
            {
                config.Bind(s => s.Signin, t => t.Endpoint.Signin);
                config.Bind(s => s.Signout, t => t.Endpoint.Signout);
            });

            
        }

        protected virtual void Remove<TAggregateRoot>(string id) where TAggregateRoot : IDocument, new()
        {
            //var aggregate = this._documentStore.Find<TAggregateRoot>(id.ToIdentity());
            //dynamic dynamic = aggregate; ;
            //dynamic.Destory();
            //this._documentStore.Save(aggregate);
            this._documentStore.Remove(id.ToIdentity());
        }

        public virtual void Update<TDto, TAggregateRoot>(TDto dto) where TDto : class where TAggregateRoot : IDocument, new()
        {
            var @object = dto.To<TAggregateRoot>();
            var aggregate = this._documentStore.Find<TAggregateRoot>(@object.Identity);
            dynamic dynamic = aggregate;
            dynamic.Change(@object);
            this._documentStore.Save(aggregate);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="TDto">查询对象</typeparam>
        /// <param name="paging">分页对象</param>
        /// <param name="specification">查询对象</param>
        /// <param name="pagingBefore">分页前</param>
        /// <param name="pagingAfter">分页后</param>
        /// <returns></returns>
        public virtual IPagingResult<TDto> Paging<TDto>(IPaging paging,
            ISpecification<TDto> specification,
            Func<IQueryable<TDto>, IQueryable<TDto>> pagingBefore = null,
            Func<IQueryable<TDto>, IQueryable<TDto>> pagingAfter = null)
        {
            var query = this._storage.AsQueryable<TDto>();
            if (specification != null && specification.Expression != null)
            {
                query = query.Where(specification.Expression);
            }

            paging.TotalNumber = query.Count();

            if (pagingBefore != null)
            {
                query = pagingBefore(query);
            }

            query = query.Skip((paging.PageIndex - 1) * paging.PageSize)
                         .Take(paging.PageSize);
            if (pagingAfter != null)
            {
                query = pagingAfter(query);
            }
            return PagingResult.Build(paging, query.ToList());
        }
    }
}
