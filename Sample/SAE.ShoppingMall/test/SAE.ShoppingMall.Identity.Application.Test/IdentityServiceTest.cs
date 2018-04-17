using SAE.ShoppingMall.Identity.Dto;
using SAE.ShoppingMall.Test;
using Xunit;
using Xunit.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using SAE.ShoppingMall.Identity.Application.Implement;
using SAE.CommonLibrary.EventStore.Document.Memory;
using SAE.CommonLibrary.MQ;
using System.Linq;
using SAE.ShoppingMall.Identity.Handle;
using SAE.ShoppingMall.Identity.DocumentService;
using SAE.ShoppingMall.Identity.DocumentService.Implement;
using SAE.ShoppingMall.DocumentService;
using SAE.CommonLibrary.EventStore.Document;

namespace SAE.ShoppingMall.Identity.Application.Test
{
    public class IdentityServiceTest : BaseTest
    {
        
        private readonly IIdentityService _identityService;
        private readonly IUserQueryServer _userQueryServer;
        public IdentityServiceTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            
            this._services.AddSingleton<IIdentityService, IdentityService>()
                          .AddSingleton<IUserQueryServer, UserDocumentServer>()
                          .AddSingleton<IDocumentServer<UserDto>,UserDocumentServer>()
                          .AddMemberDocument()
                          .AddSingleton<IDocumentEvent, DocumentPublish>()
                          .AddMemoryMQ()
                          .AddMemoryStorage()
                          .AddSingleton<RegisterHandle>();
            var provider = this._services.BuildServiceProvider();
            this._identityService = provider.GetService<IIdentityService>();
            this._userQueryServer = provider.GetService<IUserQueryServer>();
            var mq = provider.GetService<IMQ>();
            mq.SetServiceFactory(provider.GetService);
            mq.SubscibeType<RegisterHandle>();

        }

        [Theory]
        [InlineData("pjbname", "6666")]
        public UserDto Register_User(string loginName, string password)
        {
            var user = this._identityService.Register(new CredentialsDto
            {
                Name = loginName,
                Password = password
            });
            this.Show(user);
            var userDto = this._userQueryServer.Find(loginName);
            Assert.Equal(user.Credentials.Name,userDto.Credentials.Name);
            return user;
        }
    }
}
