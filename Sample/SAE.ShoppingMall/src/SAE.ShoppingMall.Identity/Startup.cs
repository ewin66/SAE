using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SAE.ShoppingMall.Identity.Application;
using SAE.ShoppingMall.Identity.Dto;

namespace SAE.ShoppingMall.Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});
            services.AddApplicationService();
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.ApplicationServices
               .UseApplicationService();

            if (env.IsDevelopment())
            {
                var provider = app.ApplicationServices;
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

                identityService.Create(new CredentialsDto
                {
                    Name = "mypjb1994",
                    Password = "Aa123456"
                });
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
           

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
