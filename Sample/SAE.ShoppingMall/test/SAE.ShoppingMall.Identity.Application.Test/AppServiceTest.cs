using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using SAE.ShoppingMall.Identity.DocumentService;
using SAE.ShoppingMall.Identity.Dto;
using SAE.ShoppingMall.Test;
using Xunit;
using Xunit.Abstractions;

namespace SAE.ShoppingMall.Identity.Application.Test
{
    public class AppServiceTest:ApplicationTest
    {
        public AppServiceTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="appId"></param>
        /// <param name="secret"></param>
        /// <param name="signin"></param>
        /// <param name="signout"></param>
        /// <returns></returns>
        [Theory]
        [InlineData("mall","mall_nb_666","mall_secrect_666","/login","/logout")]
        public AppDto Register(string name,string appId,string appSecret,string signin,string signout)
        {
            var appDto = this._appService.Register(new AppDto
            {
                AppId = appId,
                AppSecret = appSecret,
                Name = name,
                Signin = signin,
                Signout = signout,
            });
            this.Show(appDto);
            return appDto;
        }

        [Theory]
        [InlineData("mall_change", "mall_nb_88888","/oauth/login", "/oauth/logout",2)]
        public void Change(string name, string appSecret, string signin, string signout,int status)
        {
            var dto = this.Register("mall", "mall_nb_666", "mall_secrect_666", "/login", "/logout");
            dto.Name = name;
            dto.AppSecret= appSecret;
            dto.Signin= signin;
            dto.Signout = signout;
            dto.Status = status;
            this._appService.Change(dto);
            this.Show(dto);
        }
    }
}
