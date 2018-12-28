using SAE.ShoppingMall.Identity.Dto;
using Xunit;
using Xunit.Abstractions;

namespace SAE.ShoppingMall.Identity.Application.Test
{
    public class AppServiceTest:ApplicationTest
    {
        public AppServiceTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {

        }

        
        [Fact]
        public AppDto Register()
        {
            var url = $"www.{this.Random()}.com";
            var appDto = this._appService.Register(new AppDto
            {
                AppId = this.Random(),
                AppSecret = this.Random(),
                Name = $"test_app_{this.Random()}",
                Signin = $"{url}/login",
                Signout = $"{url}/logut",
            });
            this.Show(appDto);
            return appDto;
        }

        [Fact]
        public void Change()
        {
            var url = $"www.{this.Random()}.com";
            var dto = this.Register();
            dto.Name = "update_app";
            dto.AppSecret= this.Random();
            dto.Signin= this.Random();
            dto.Signin = $"{url}/login";
            dto.Signout = $"{url}/logut";
            this._appService.Change(dto);
            this.Show(dto);
        }
    }
}
