using SAE.CommonComponents.OAuth.Application.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.CommonComponents.OAuth.Models
{
    public class LoginModel
    {
        public LoginModel()
        {
            this.ReturnUrl = "~/";
        }
        [Required(ErrorMessage ="请输入:\"{0}\"")]
        [Display(Name="用户名")]
        public string Name { get; set; }
        [Required(ErrorMessage = "请输入:\"{0}\"")]
        [Display(Name = "密码")]
        public string Password { get; set; }

        
        public static implicit operator LoginDto(LoginModel self)
        {
            return new LoginDto
            {
                Name = self.Name,
                Password = self.Password
            };
        }

        public string ReturnUrl
        {
            get;set;
        }
    }
}
