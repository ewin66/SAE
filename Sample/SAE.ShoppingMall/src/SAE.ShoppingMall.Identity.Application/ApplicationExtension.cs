using SAE.CommonLibrary.EventStore.Document.Memory;
using SAE.CommonLibrary.EventStore.Queryable;
using SAE.CommonLibrary.EventStore.Queryable.Builder;
using SAE.CommonLibrary.MQ;
using SAE.ShoppingMall.Identity.Domain;
using SAE.ShoppingMall.Identity.Domain.Event;
using SAE.ShoppingMall.Identity.Dto;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {

            services.AddMemoryMQ()
                    .AddMemoryStorage()
                    .AddMemberDocument()
                    .AddDefaultTransferService()
                    .AddStoragePersistenceService()
                    .AddDefaultHandler();
                    
                   
            return services;
        }

        public static IServiceProvider UseApplicationService(this IServiceProvider provider)
        {
            provider.UseServiceProvider();

            var mq = provider.GetService<IMQ>();
            mq.CreateBuilder()
              .RegisterAssembly(typeof(IAggregateRoot).Assembly)
              .Mapping<UserDto>()
              .Mapping<UserRegisterEvent>(HandlerEnum.Add)
              .Mapping(HandlerEnum.Update,t=> t.Name.StartsWith($"Change{nameof(User)}"))
              .Mapping<AppDto>()
              .Mapping<AppCreateEvent>(HandlerEnum.Add)
              .Mapping(HandlerEnum.Update, t => t.Name.StartsWith($"Change{nameof(App)}"))
              .Mapping<Permission>()
              .Mapping(HandlerEnum.Update, t => t.Name.StartsWith($"Change{nameof(Permission)}"))
              .Mapping<RoleDto>()
              .Mapping(HandlerEnum.Update,t => t.Name.StartsWith($"Change{nameof(Role)}"))
              .RegistrationBuilder
              .Build();
              
            provider.UseServiceProvider();

            

            //var appService = provider.GetService<IAppService>();
            //var identityService = provider.GetService<IIdentityService>();

            //appService.Register(new AppDto
            //{
            //    AppId = "sae.admin.com",
            //    AppSecret = "sae.admin.secret",
            //    Name = "SAE Admin",
            //    Signin = "http://admin.sae.com:12002/signin-oidc",
            //    Signout = "http://admin.sae.com:12002/signout"
            //});

            //identityService.Register(new CredentialsDto
            //{
            //    Name = "mypjb1994",
            //    Password = "Aa123456"
            //});

            return provider;
        }
    }
}
