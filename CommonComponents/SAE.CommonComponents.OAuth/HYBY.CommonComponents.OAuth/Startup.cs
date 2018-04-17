using SAE.CommonComponents.OAuth.Code;
using SAE.CommonLibrary.OAuth;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace SAE.CommonComponents.OAuth
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IReferenceTokenStore, TokenStore>()
                    .AddSingleton<IPersistedGrantStore, RedisPersistedGrantStore>()
                    .AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddClientStore<ClientStore>()
                    .AddResourceStore<ResourceStore>()
                    .AddProfileService<ProfileService>();

            //services.AddDependencyInjectionConfiguration(this.Configuration.GetSection("DependencyInjection"))
            //        .TryAddSingleton(this.Configuration.GetSection("Redis").Get<RedisConfig>());

            services/*.AddLogger()*/
                    .AddJson();
                    //.AddStorage()
                    //.AddCache()
                    //.TryAddSingleton(this.Configuration.GetSection("MongoDB").Get<MongoDBConfig>());
                           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            if (env.IsDevelopment())
            {

                loggerFactory.AddDebug();
                
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/home/error");
            }

            app.UseStatusCodePages()
               .UseStaticFiles()
               .UseIdentityServer()
               .UseMvc(routes =>
               {
                   routes.MapRoute(
                       name: "default",
                       template: "{controller=Home}/{action=Index}/{id?}");
               });
        }
    }
}
