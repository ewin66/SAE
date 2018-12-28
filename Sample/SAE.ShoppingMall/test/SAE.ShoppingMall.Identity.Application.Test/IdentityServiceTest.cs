using SAE.ShoppingMall.Identity.Dto;
using Xunit;
using Xunit.Abstractions;
using System.Linq;
using System;
using SAE.CommonLibrary.Common;
using SAE.CommonLibrary.MQ;
using SAE.CommonLibrary.EventStore.Queryable.Handle;
using SAE.ShoppingMall.Identity.Domain.Event;

namespace SAE.ShoppingMall.Identity.Application.Test
{
    public class IdentityServiceTest : ApplicationTest
    {
        public IdentityServiceTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
           
        }

        [Fact]
        public UserDto Register_User()
        {
            var dto = new CredentialsDto
            {
                Name = this.Random(),
                Password = this.Random()
            };
            this._identityService.Create(dto);
            var userDto = this._identityService.GetByLoginName(dto.Name);
            this.Show(userDto);
            var user = this._storage.AsQueryable<UserDto>()
                                       .FirstOrDefault(s => s.Credentials.Name == dto.Name);
            Assert.NotNull(user);
            Assert.Equal(user.Credentials.Name, userDto.Credentials.Name);
            return user;
        }

        [Fact]
        public void Login()
        {
            var dto = this.Register_User();

            var user = this._identityService.Authentication(dto.Credentials);
            this.Show(user);
            Assert.Equal(dto.Credentials.Name, user.Credentials.Name);

        }
        [Fact]
        public void ChangeInfo()
        {
            var dto = this.Register_User();
            dto.Information = new UserInfoDto
            {
                QQ = this.Random().Substring(0, 9),
                Phone = this.Random().Substring(0, 11),
                BirthDate = DateTime.Now,
                Email = $"{this.Random()}.@sae.com",
                Sex = 1,
                Hometown = "中国"
            };
            this._identityService.Update(dto);
            dto.Information.Phone += "1";
            var user = this._identityService.GetById(dto.Id);
            Assert.Equal(dto.Information.BirthDate, user.Information.BirthDate);
            Assert.Equal(dto.Information.Email, user.Information.Email);
            Assert.Equal(dto.Information.QQ, user.Information.QQ);
            Assert.NotEqual(dto.Information.Phone, user.Information.Phone);
            Assert.Equal(dto.Information.Sex, user.Information.Sex);
        }
    }
}
