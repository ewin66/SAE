using SAE.Test.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using SAE.CommonLibrary.Json;

namespace SAE.CommonLibrary.Http.Test
{
    public class HttpRestTest
    {
        private readonly ITestOutputHelper _outpub;
        public const string Url = "http://www.test1.com";
        public PathString PutPath = "/api/edit";
        public PathString GetPath = "/api/value"; 
        public PathString PostPath = "/api/upload";
        public PathString DeletePath = "/api/delete";
        public PathString OptionsPath = "/api/create";
        private readonly HttpClient _client;
        private readonly Student _student;
        private readonly Dictionary<string, string> _dic;
        public HttpRestTest(ITestOutputHelper outpub)
        {
            _dic = new Dictionary<string, string>
            {
                {"name","01111" },
                { "age","2017"},
            };
            _outpub = outpub;
            var builder = new WebHostBuilder()
                .UseUrls(Url)
                .ConfigureServices(services =>
                {
                    services.AddMvc();
                })
                .Configure(app =>
                {
                    app.UseMvc();
                });

            var server = new TestServer(builder);
            _client = server.CreateClient();
            HttpRequestMessageExtension.SetHttpClient(_client);
            _student = new Student
            {
                Name = "mypjb"
            };
        }


        

        [Fact]
        public async Task Get()
        {
            var response = await HttpRest.Get(Url + GetPath + "?cctv=bbq&non=1119")
                                         .AddJsonContent(_student)
                                         .SendAsync();
            await OutPutResult(response);
        }


        [Fact]
        public async Task Post()
        {

            var response = await HttpRest.Post(Url + PostPath + "?cctv=bbq&non=1119")
                                         .AddContent(new FileInfo(Path.Combine(AppContext.BaseDirectory, "appsettings.text")))
                                         .AddContent(new FileInfo(Path.Combine(AppContext.BaseDirectory, "appsettings.text")))
                                         .AddContent(new FileInfo(Path.Combine(AppContext.BaseDirectory, "appsettings.text")))
                                         .AddContent(new FileInfo(Path.Combine(AppContext.BaseDirectory, "appsettings.text")))
                                         .AddContent(this._dic)
                                         .SendAsync();

            //var request = HttpRest.Post(Url + PostPath + "?cctv=bbq&non=1119");

            await OutPutResult(response);
        }

        [Fact]
        public async Task Put()
        {
            var response = await HttpRest.Put(Url + PutPath + "?cctv=bbq&non=1119")
                                         .AddJsonContent(this._student)
                                         .SendAsync();
            await OutPutResult(response);
        }

        [Fact]
        public async Task Delete()
        {
            var response = await HttpRest.Delete(Url + DeletePath + "?cctv=bbq&non=1119")
                                         .AddJsonContent(this._student)
                                         .SendAsync();
            await OutPutResult(response);
        }

        [Fact]
        public async Task Options()
        {
            var response = await HttpRest.Options(Url + OptionsPath + "?cctv=bbq&non=1119")
                                         .AddJsonContent(this._student)
                                         .SendAsync();
            await OutPutResult(response);
        }

        private async Task OutPutResult(System.Net.Http.HttpResponseMessage response)
        {
            var resultModel = await response.AsModelAsync<object>();

            var resultStream = await response.AsStreamAsync();

            var resultString = await response.AsStringAsync();

            var json = JsonHelper.Serialize(resultModel);

            var bytes = new byte[resultStream.Length];

            await resultStream.ReadAsync(bytes, 0, bytes.Length);

            _outpub.WriteLine("Model:" + json);

            _outpub.WriteLine("Stream:" + Encoding.UTF8.GetString(bytes));

            _outpub.WriteLine("String:" + Json.JsonHelper.Serialize(resultString));

            Assert.Equal(json, resultString.Trim());
        }
    }
}
