using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SAE.CommonLibrary.EventStore.Document;
using SAE.CommonLibrary.MQ;
using SAE.CommonLibrary.Storage;
using SAE.ShoppingMall.Test;
using System;
using System.Security.Cryptography;
using System.Text;
using Xunit.Abstractions;

namespace SAE.ShoppingMall.Identity.Application.Test
{
    public abstract class ApplicationTest : BaseTest
    {
        protected readonly IIdentityService _identityService;
        protected readonly IAppService _appService;
        protected readonly IStorage _storage;
        protected readonly IServiceProvider _provider;
        public ApplicationTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var containerBuilder= new ContainerBuilder();
            containerBuilder.RegisterAssemblyTypes(typeof(ApplicationExtension).Assembly)
                            .AsImplementedInterfaces()
                            .SingleInstance();

            this.Services.AddApplicationService();

            containerBuilder.Populate(this.Services);
            
            this._provider = new AutofacServiceProvider(containerBuilder.Build())
                                .UseApplicationService()
                                .UseDefaultDocumentPublish()
                                .UseServiceProvider();

            this._identityService = _provider.GetService<IIdentityService>();

            this._appService = _provider.GetService<IAppService>();

            this._storage = _provider.GetService<IStorage>();
        }

        /// <summary>
        /// 随机数
        /// </summary>
        /// <returns></returns>
        protected string Random()
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                string text = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(Guid.NewGuid().ToString("N"))), 4, 8);
                text = text.Replace("-", "").ToLower();
                return text;
            }
        }
        
    }
}
