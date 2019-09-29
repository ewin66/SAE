using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using SAE.CommonLibrary.Http;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SAE.CommonLibrary.Trace.Test
{
    public class TraceTest: IDisposable
    {
        private readonly ITestOutputHelper _outpub;
        private readonly HttpClient _client;
        public const string Url= "http://www.test1.com";
        public const string ApiUrl = "http://api.text.net";
        public TestServer _server;
        public IApplicationBuilder UseApp(IApplicationBuilder app)
        {
            return app;
        }
        public TraceTest(ITestOutputHelper outpub)
        {
            var apiBuilder = new WebHostBuilder().UseUrls(ApiUrl)
                                                .ConfigureServices(serviceCollection =>
                                                {
                                                    serviceCollection.AddTrace();
                                                }).Configure(app =>
                                                {
                                                    app.UseTrace()
                                                       .UseTraceRpc()
                                                       .Map("/api/values", config =>
                                                        {
                                                            config.Run(async context =>
                                                            {
                                                                var result = "{name:'pjb'}";
                                                                var bytes = Encoding.UTF8.GetBytes(result);
                                                                await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                                                            });

                                                        })
                                                        .Map("/api/error", config =>
                                                        {
                                                            config.Run(context =>
                                                            {
                                                                throw new Exception($"this is api exception url:{context.Request.Host}");
                                                            });
                                                        });

                                                });
            var apiHandle = new TestServer(apiBuilder).CreateHandler();
            _outpub = outpub;
            var builder = new WebHostBuilder()
                .UseUrls(Url)
                .ConfigureServices(serviceCollection =>
                {
                    serviceCollection.AddTrace();
                })
                .Configure(app =>
                {
                    app.UseTrace()
                       .UseTraceRpc(apiHandle)
                       .Map("/get", config =>
                       {
                           config.Run(async context =>
                           {
                               var stopTime = context.Request.Query["stopTime"];
                               int millisecondsTimeout;
                               int.TryParse(stopTime, out millisecondsTimeout);
                               Thread.Sleep(millisecondsTimeout < 0 ? 0 : millisecondsTimeout);
                               var result = $"This Is Test Stop Milliseconds:{millisecondsTimeout}";
                               var bytes = Encoding.UTF8.GetBytes(result);
                               await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);

                           });

                       })
                       .Map("/error", config =>
                        {
                            config.Run(context =>
                            {
                                throw new Exception($"this is error url:{context.Request.Host}");
                            });
                        })
                        .Map("/getValue", config =>
                         {
                             config.Run(async context =>
                             {
                                 Thread.Sleep(1000);
                                 var reponse = await HttpRest.Get($"{ApiUrl}/api/values")
                                                             .SendAsync();
                                 var result = await reponse.AsStringAsync();
                                 var bytes = Encoding.UTF8.GetBytes(result);
                                 await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                             });
                         }).Map("/getError", config =>
                         {
                             config.Run(async context =>
                             {
                                 var reponse = await HttpRest.Get($"{ApiUrl}/api/error")
                                                             .SendAsync();
                                 var result = await reponse.AsStringAsync();
                                 var bytes = Encoding.UTF8.GetBytes(result);
                                 await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                             });
                         });

                });

            _server = new TestServer(builder);

            _client = _server.CreateClient();
        }


        [Theory]
        [InlineData(1001)]
        [InlineData(1000)]
        [InlineData(500)]
        public async Task Get(int millisecondsTimeout)
        {
            await this.Request($"{Url}/get?stopTime={millisecondsTimeout}");
            Thread.Sleep(1000 * 2);
        }
        [Fact]
        public async Task Error()
        {
            await this.Request($"{Url}/error?guid={Guid.NewGuid().ToString()}");
            Thread.Sleep(1000 * 2);
        }


        [Fact]
        public async Task ApiValue()
        {
            await this.Request($"{Url}/getValue?guid={Guid.NewGuid().ToString()}");
            Thread.Sleep(1000 * 2);
        }

        [Fact]
        public async Task ApiError()
        {
            await this.Request($"{Url}/getError?guid={Guid.NewGuid().ToString()}");
            Thread.Sleep(1000 * 2);
        }




        public async Task Request(string url)
        {
            _outpub.WriteLine($"Start:{url}");
            try
            {
                var result = await _client.GetStringAsync(url);
                _outpub.WriteLine(result);
            }
            catch(Exception ex)
            {
                _outpub.WriteLine(ex.Message);
            }
            
            
        }

        public void Dispose()
        {
            _server.Dispose();
        }
    }
}
