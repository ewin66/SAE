using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using SAE.CommonLibrary.EventStore.Document;
using SAE.CommonLibrary.EventStore.Document.Memory;
using SAE.CommonLibrary.MQ;
using SAE.ShoppingMall.DocumentService;
using SAE.ShoppingMall.Identity.Application.Implement;
using SAE.ShoppingMall.Identity.DocumentService;
using SAE.ShoppingMall.Identity.DocumentService.Implement;
using SAE.ShoppingMall.Identity.Dto;
using SAE.ShoppingMall.Identity.Handle;
using SAE.ShoppingMall.Test;
using Xunit.Abstractions;

namespace SAE.ShoppingMall.Identity.Application.Test
{
    public abstract class ApplicationTest : BaseTest
    {
        protected readonly IIdentityService _identityService;
        protected readonly IUserQueryService _userQueryServer;
        protected readonly IAppService _appService;
        protected readonly IAppQueryService _appQueryService;
        public ApplicationTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            this._services.AddSingleton<IIdentityService, IdentityService>()
                          .AddSingleton<IUserQueryService, UserDocumentService>()
                          .AddSingleton<IAppService,AppService>()
                          .AddSingleton<IAppQueryService,AppDocuemntService>()
                          .AddSingleton<IDocumentService<UserDto>, UserDocumentService>()
                          .AddMemberDocument()
                          .AddSingleton<IDocumentEvent, DocumentPublish>()
                          .AddMemoryMQ()
                          .AddMemoryStorage()
                          .AddSingleton<UserHandle>();

            var provider = this._services.BuildServiceProvider();
            this._identityService = provider.GetService<IIdentityService>();
            this._userQueryServer = provider.GetService<IUserQueryService>();
            this._appService = provider.GetService<IAppService>();
            this._appQueryService = provider.GetService<IAppQueryService>();
            var mq = provider.GetService<IMQ>();
            mq.SetServiceFactory(provider.GetService);
            mq.SubscibeAssembly(typeof(UserHandle).Assembly);
        }
    }
}
