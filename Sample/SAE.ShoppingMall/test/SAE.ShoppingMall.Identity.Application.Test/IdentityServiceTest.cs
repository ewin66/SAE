using SAE.ShoppingMall.Identity.Dto;
using Xunit;
using Xunit.Abstractions;

namespace SAE.ShoppingMall.Identity.Application.Test
{
    public class IdentityServiceTest : ApplicationTest
    {
        public IdentityServiceTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
         
            

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

        [Theory]
        [InlineData("mypjb1994","6666666666")]
        public void Login(string loginName,string password)
        {
            var userDto = this.Register_User(loginName, password);
            
            var user = this._identityService.Login(new CredentialsDto
            {
                Name = loginName,
                Password = password
            });
            this.Show(user);
            Assert.Equal(userDto.Credentials.Name,user.Credentials.Name);
            
        }
        [Theory]
        [InlineData("mypjb1994", "6666666666")]
        public void ChangeInfo(string loginName, string password)
        {
            var userDto = this.Register_User(loginName, password);
        }
    }
}
