using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SAE.CommonLibrary.EventStore.Document;
using SAE.CommonLibrary.MQ;
using SAE.CommonLibrary.Storage;
using SAE.ShoppingMall.Test;
using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace SAE.ShoppingMall.Identity.Application.Test
{
    public abstract class ApplicationTest : BaseTest
    {
        protected readonly IIdentityService _identityService;
        protected readonly IAppService _appService;
        protected readonly IServiceProvider _provider;
        public ApplicationTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            //var containerBuilder= new ContainerBuilder();
            //containerBuilder.RegisterAssemblyTypes(typeof(ApplicationExtension).Assembly)
            //                .AsImplementedInterfaces()
            //                .SingleInstance();

            this._services.AddApplicationService();

            ////containerBuilder.Populate(this.Services);

            this._provider = this._services.BuildServiceProvider()
                             //new AutofacServiceProvider(containerBuilder.Build())
                             .UseApplicationService();


            this._identityService = _provider.GetService<IIdentityService>();
            
            this._appService = _provider.GetService<IAppService>();

        }



    }
}
