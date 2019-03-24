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
            var dto = new AppDto
            {
                AppId = this.Random(),
                AppSecret = this.Random(),
                Name = $"test_app_{this.Random()}",
                Signin = $"{url}/login",
                Signout = $"{url}/logut",
            };
            this._appService.Register(dto);
            
            this.Valid(dto);
            return dto;
        }

        private void Valid(AppDto dto)
        {
            var app = this._appService.GetById(dto.AppId);
            Assert.NotNull(app);
            this.Show(app);
            Assert.Equal(dto.AppId, app.AppId);
            Assert.Equal(dto.AppSecret, app.AppSecret);
            Assert.NotEqual(dto.CreateTime, app.CreateTime);
            Assert.Equal(dto.Name, app.Name);
            Assert.Equal(dto.Signin, app.Signin);
            Assert.Equal(dto.Signout, app.Signout);
        }

        [Fact]
        public void Change()
        {
            var url = $"www.{this.Random()}.com";
            var dto = this.Register();
            dto.Name = "update_app";
            dto.AppSecret= this.Random();
            dto.Signin = $"{url}/login";
            dto.Signout = $"{url}/logut";
            this._appService.Change(dto);
            this.Valid(dto);
        }
        [Fact]
        public void Remove()
        {
            var dto = this.Register();
            this._appService.Remove(dto.AppId);
            var app = this._appService.GetById(dto.AppId);
            Assert.Null(app);
        }
    }
}
