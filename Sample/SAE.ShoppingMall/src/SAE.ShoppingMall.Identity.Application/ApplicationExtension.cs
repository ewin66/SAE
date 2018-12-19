using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using SAE.CommonLibrary.EventStore.Document;
using SAE.CommonLibrary.EventStore.Document.Memory;
using SAE.CommonLibrary.Ioc.ServiceCollections;
using SAE.CommonLibrary.MQ;
using SAE.ShoppingMall.DocumentService;
using SAE.ShoppingMall.Identity.Application;
using SAE.ShoppingMall.Identity.DocumentService.Implement;
using SAE.ShoppingMall.Identity.Dto;
using SAE.ShoppingMall.Identity.Handle;
using SAE.CommonLibrary.EventStore.Queryable;
using System.Reflection;
using SAE.ShoppingMall.Identity.Domain;
using SAE.ShoppingMall.Identity.Domain.Event;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            
            services.AddRelative(ServiceLifetime.Singleton)
                    .AddRelative("SAE.ShoppingMall", ServiceLifetime.Singleton)
                    .AddMemoryStorage()
                    .AddMemberDocument()
                    .AddSingleton<IDocumentService<UserDto>, UserDocumentService>()
                    .AddSingleton<IDocumentService<AppDto>, AppDocuemntService>()
                    .AddSingleton<UserHandle>()
                    .AddSingleton<AppHandle>()
                    .AddSingleton<IDocumentEvent, DocumentPublish>()
                    .AddDefaultTransferService()
                    .AddMemoryPersistenceService()
                    .AddQueryableHandler();
                   
            return services;
        }

        public static IServiceProvider UseApplicationService(this IServiceProvider provider)
        {
            provider.UseServiceProvider();

            var mq = provider.GetService<IMQ>();
            mq.CreateQueryableBuilder()
              .RegisterAssembly(typeof(IAggregateRoot).Assembly);
              
              

            mq.UseServiceFactory(provider.GetService);

            //mq.SubscibeAssembly(typeof(UserHandle).Assembly);

            var appService = provider.GetService<IAppService>();
            var identityService = provider.GetService<IIdentityService>();

            appService.Register(new AppDto
            {
                AppId = "sae.admin.com",
                AppSecret = "sae.admin.secret",
                Name = "SAE Admin",
                Signin = "http://admin.sae.com:12002/signin-oidc",
                Signout = "http://admin.sae.com:12002/signout"
            });

            identityService.Register(new CredentialsDto
            {
                Name = "mypjb1994",
                Password = "Aa123456"
            });

            return provider;
        }
    }
}
