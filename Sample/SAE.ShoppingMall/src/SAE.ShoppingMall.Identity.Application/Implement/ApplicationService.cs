using SAE.CommonLibrary.EventStore.Document;
using SAE.CommonLibrary.ObjectMapper;
using SAE.CommonLibrary.Storage;
using SAE.ShoppingMall.Identity.Domain;
using SAE.ShoppingMall.Identity.Domain.Event;
using SAE.ShoppingMall.Identity.Domain.ValueObject;
using SAE.ShoppingMall.Identity.Dto;

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
            TinyMapper.Bind<RegisterUserEvent, UserDto>(config =>
            {
                 config.Bind(s => s.CreateTime, t => t.CreateTime);
                 config.Bind(s => s.Id, t => t.Id);
                 config.Bind(s => s.LoginName, t => t.Credentials.Name);
                 config.Bind(s => s.Password, t => t.Credentials.Password);
                 config.Bind(s => s.Status, t => t.Status);
            });

            TinyMapper.Bind<RegisterUserEvent, User>(config =>
            {
                config.Bind(s => s.Id, t => t.Id);
                config.Bind(s => s.LoginName, t => t.Credentials.Name);
                config.Bind(s => s.Password, t => t.Credentials.Password);
                config.Bind(s => s.Salt, t => t.Credentials.Salt);
                config.Bind(s => s.Status, t => t.Status);
                config.Bind(s => s.CreateTime, t => t.CreateTime);
            });

            TinyMapper.Bind<ChangeUserInfoEvent, User>(config =>
            {
                config.Bind(s => s.BirthDate, t => t.Information.BirthDate);
                config.Bind(s => s.Email, t => t.Information.Contact.Email);
                config.Bind(s => s.Hometown, t => t.Information.Hometown);
                config.Bind(s => s.Phone, t => t.Information.Contact.Phone);
                config.Bind(s => s.QQ, t => t.Information.Contact.QQ);
                config.Bind(s => s.Sex, t => t.Information.Sex);
            });
            TinyMapper.Bind<UserInfo, ChangeUserInfoEvent>(config =>
            {
                config.Bind(s => s.BirthDate, t => t.BirthDate);
                config.Bind(s => s.Contact.Email, t => t.Email);
                config.Bind(s => s.Hometown, t => t.Hometown);
                config.Bind(s => s.Contact.Phone, t => t.Phone);
                config.Bind(s => s.Contact.QQ, t => t.QQ);
                config.Bind(s => s.Sex, t => t.Sex);
            });

            TinyMapper.Bind<UserDto, User>(config =>
            {
                config.Bind(s => s.Information.Email, t => t.Information.Contact.Email);
                config.Bind(s => s.Information.Phone, t => t.Information.Contact.Phone);
                config.Bind(s => s.Information.QQ, t => t.Information.Contact.QQ);
            }).Reverse();

            TinyMapper.Bind<RegisterAppEvent, AppDto>(config =>
            {
                config.Bind(s => s.Id, t => t.AppId);
                config.Bind(s => s.Name, t => t.Name);
                config.Bind(s => s.Secret, t => t.AppSecret);
                config.Bind(s => s.Signin, t => t.Signin);
                config.Bind(s => s.Signout, t => t.Signout);
                config.Bind(s => s.Status, t => t.Status);
                config.Bind(s => s.CreateTime, t => t.CreateTime);
            });
        }
    }
}
