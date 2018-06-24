using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SAE.CommonLibrary.EventStore.Document;
using SAE.CommonLibrary.Ioc.ServiceCollections;
using SAE.ShoppingMall.DocumentService;
using SAE.ShoppingMall.Identity.DocumentService.Implement;
using SAE.ShoppingMall.Identity.Dto;
using SAE.ShoppingMall.Identity.Handle;

namespace SAE.ShoppingMall.Identity.API
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
            services.AddMvc();
            services.AddApplicationService()
                    .AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole(LogLevel.Debug);
                app.UseDeveloperExceptionPage();
            }
            app.ApplicationServices.UseApplicationService();
            app.UseCors(builder =>
               {
                   builder.WithOrigins("*")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowAnyOrigin();
               })
               .UseMvc();
        }
    }
}
