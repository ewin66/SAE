using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using SAE.CommonLibrary.Common;
using SAE.CommonLibrary.Json;
using SAE.Test.Infrastructure;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SAE.CommonComponent.Upload.Test
{
    public class UploadTest : BaseTest
    {
        private readonly IJsonConvertor _jsonConvertor;
        private readonly HttpClient _client;
        public const string Host = "http://file.sae.com";
        public UploadTest(ITestOutputHelper output) : base(output)
        {
            _jsonConvertor = new ServiceCollection().AddJson().BuildServiceProvider().UseServiceFacade().GetService<IJsonConvertor>();
            var builder = new WebHostBuilder()
                .UseUrls(Host)
                .UseWebRoot("wwwroot")
                .UseStartup<Startup>();

            var server = new TestServer(builder);
            this._client = server.CreateClient();
        }

        [Fact]
        public async Task Upload()
        {
            var content = new MultipartFormDataContent();
            var files = Directory.GetFiles("./files")
                                 .Select(path => new FileStream(path, FileMode.Open))
                                 .ForEach(s =>
                                 {
                                     var fileName = Path.GetFileName(s.Name);
                                     content.Add(new StreamContent(s), Path.GetFileNameWithoutExtension(s.Name), fileName
                                 );
                                 });


            var httpResponse = await this._client.PostAsync($"/upload", content);
            httpResponse.EnsureSuccessStatusCode();
            var json = await httpResponse.Content.ReadAsStringAsync();
            
            var result = _jsonConvertor.Deserialize<StandardResult<List<Code.FileResult>>>(json);
            foreach(var file in result.Body)
            {
                var response = await this._client.GetAsync(file.Url);
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                Assert.True(stream.Length > 0);
                this.Show(new { Descript = file, stream.Length });
            }
            
        }
    }
}
