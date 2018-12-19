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
using SAE.CommonLibrary.Ioc.ServiceCollections;

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

            this._services.AddApplicationService();

            var provider = this._services.BuildServiceProvider();

            provider.UseApplicationService()
                    .UseDefaultDocumentPublish()
                    .UseServiceProvider();
                    
            this._identityService = provider.GetService<IIdentityService>();
            this._userQueryServer = provider.GetService<IUserQueryService>();
            this._appService = provider.GetService<IAppService>();
            this._appQueryService = provider.GetService<IAppQueryService>();

            provider.UseApplicationService();
        }
    }
}
