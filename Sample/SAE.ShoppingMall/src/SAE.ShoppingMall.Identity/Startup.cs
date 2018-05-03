using System;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SAE.CommonLibrary.Cache.Redis;
using SAE.CommonLibrary.EventStore.Document;
using SAE.CommonLibrary.EventStore.Document.Memory;
using SAE.CommonLibrary.MQ;
using SAE.ShoppingMall.DocumentService;
using SAE.ShoppingMall.Identity.Application;
using SAE.ShoppingMall.Identity.Application.Implement;
using SAE.ShoppingMall.Identity.Code;
using SAE.ShoppingMall.Identity.DocumentService;
using SAE.ShoppingMall.Identity.DocumentService.Implement;
using SAE.ShoppingMall.Identity.Dto;
using SAE.ShoppingMall.Identity.Handle;

namespace SAE.ShoppingMall.Identity
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddCache()
                    .AddJson()
                    .AddLogger()
                    .AddMemoryStorage()
                    .AddMemoryMQ()
                    .AddMemberDocument()
                    .AddSingleton<IIdentityService, IdentityService>()
                    .AddSingleton<IUserQueryService, UserDocumentService>()
                    .AddSingleton<IAppService, AppService>()
                    .AddSingleton<IAppQueryService, AppDocuemntService>()
                    .AddSingleton<IDocumentService<UserDto>, UserDocumentService>()
                    .AddSingleton<IDocumentEvent, DocumentPublish>()
                    .AddSingleton<UserHandle>()
                    .AddSingleton(new RedisConfig
                    {
                        Connection= "redis.cache.com:6379,allowadmin=true,syncTimeout=5000"
                    })
                    .AddMvc();

            services.AddSingleton<IPersistedGrantStore, RedisPersistedGrantStore>()
                    .AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddClientStore<ClientStore>()
                    .AddResourceStore<ResourceStore>()
                    .AddProfileService<ProfileService>();
                    
            
                    
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole()
                         .AddDebug();

            if (env.IsDevelopment())
            {
                var mq = app.ApplicationServices.GetService<IMQ>();
                mq.SetServiceFactory(app.ApplicationServices.GetService);
                mq.SubscibeAssembly(typeof(UserHandle).Assembly);

                var appService = app.ApplicationServices.GetService<IAppService>();
                var identityService = app.ApplicationServices.GetService<IIdentityService>();
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
                
            }

            app.UseStatusCodePages()
               .UseStaticFiles()
               .UseIdentityServer()
               .UseMvcWithDefaultRoute();
        }
    }
}
