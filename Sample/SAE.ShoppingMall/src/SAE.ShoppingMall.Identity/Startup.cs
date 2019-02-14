using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SAE.ShoppingMall.Identity.Application;
using SAE.ShoppingMall.Identity.Dto;
using SAE.CommonLibrary.MvcExtension;
using System;
using SAE.ShoppingMall.Identity.Code;
using System.Collections.Generic;
using IdentityServer4.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.Cookies;

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
            var profile= new IdentityResources.Profile();
            profile.UserClaims.Add("role");
            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddClientStore<ClientStore>()
                    .AddInMemoryIdentityResources(new List<IdentityResource>
                    {
                        new IdentityResources.OpenId(),
                        profile,
                    });

            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication()
                    .AddCookie();
            
            
            services.AddMvc()
                    .AddValidation()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                    AppId = "oauth.sae.com",
                    AppSecret = "oauth.sae.secret",
                    Name = "SAE Admin",
                    Signin = "https://oauth.sae.com:12001/signin-oidc",
                    Signout = "https://oauth.sae.com:12001/signout-callback-oidc"
                });

                appService.Register(new AppDto
                {
                    AppId = "www.shop.com",
                    AppSecret = "www.shop.secret",
                    Name = "SAE Shop",
                    Signin = "https://www.shop.com:12002/signin-oidc",
                    Signout = "https://www.shop.com:12002/signout-callback-oidc"
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

            app.UseStaticFiles()
               .UseIdentityServer()
               .UseAuthentication();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
