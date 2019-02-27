using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SAE.CommonComponent.Upload.Code;
using SAE.CommonLibrary.Common;
using SAE.CommonLibrary.Json;

namespace SAE.CommonComponent.Upload
{
    public class Startup
    {
        private FileConfig fileConfig;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            var build= new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
            fileConfig= build.GetSection("FileConfig").Get<FileConfig>();
            services.AddJson()
                    .AddSingleton(this.fileConfig)
                    .AddFileDecorator<NamingDecorator>()
                    .AddFileDecorator<UploadDecorator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                              IHostingEnvironment env,
                              IJsonConvertor jsonConvertor,
                              IFileDecorator fileDecorator)
        {

            app.UseStatusCodePages()
               .UseStaticFiles();

            app.Map("/upload", builder =>
            {
                builder.Run(async context =>
                {
                    var result=new StandardResult();
                    if (context.Request?.Form?.Files==null|| context.Request.Form.Files.Count <= 0)
                    {
                        result.StatusCode = StatusCode.RequestInvalid;
                        result.Message = "请上传文件!";
                    }
                    else
                    {
                        List<FileDecorator> fileDecorators = new List<FileDecorator>();
                        foreach(var fileContext in context.Request.Form.Files.Select(f=>new FileContext(f.FileName,f.OpenReadStream(),context)))
                        {
                            await fileDecorator.Decorate(fileContext);
                        }
                        result.Body = fileDecorators;
                    }
                    await context.Response.WriteAsync(jsonConvertor.Serialize(result));
                });
            });
            
        }
    }
}
