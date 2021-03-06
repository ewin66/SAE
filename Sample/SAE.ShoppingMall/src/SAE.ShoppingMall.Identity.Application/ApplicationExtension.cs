using SAE.CommonLibrary.EventStore.Document;
using SAE.CommonLibrary.EventStore.Document.Memory;
using SAE.CommonLibrary.EventStore.Queryable;
using SAE.CommonLibrary.EventStore.Queryable.Builder;
using SAE.CommonLibrary.MQ;
using SAE.ShoppingMall.Identity.Application;
using SAE.ShoppingMall.Identity.Application.Implement;
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
            services.AddSingleton<IIdentityService, IdentityService>()
                    .AddSingleton<IAppService, AppService>()
                    .AddMemoryMQ()
                    .AddMemoryStorage()
                    .AddMemberDocument()
                    .AddStoragePersistenceService()
                    .AddDefaultHandler();
                    
            return services;
        }

        public static IServiceProvider UseApplicationService(this IServiceProvider provider)
        {
            provider.UseServiceFacade()
                    .UseDefaultDocumentPublish();

            var mq = provider.GetService<IMQ>();

            mq.CreateBuilder()
              .RegisterAssembly(typeof(IAggregateRoot).Assembly)
              .Mapping<UserDto>()
              .Mapping<UserRegisterEvent>(HandlerEnum.Add)
              .Mapping(HandlerEnum.Update,t=> t.Name.StartsWith($"{nameof(User)}Change"))
              .Mapping<AppDto>()
              .Mapping<AppCreateEvent>(HandlerEnum.Add)
              .Mapping(HandlerEnum.Update, t => t.Name.StartsWith($"{nameof(App)}Change"))
              .Mapping<PermissionDto>()
              .Mapping(HandlerEnum.Update, t => t.Name.StartsWith($"{nameof(Permission)}Change"))
              .Mapping<RoleDto>()
              .Mapping(HandlerEnum.Update,t => t.Name.StartsWith($"{nameof(Role)}Change"))
              .RegistrationBuilder
              .Build();
             
            return provider;
        }
    }
}
