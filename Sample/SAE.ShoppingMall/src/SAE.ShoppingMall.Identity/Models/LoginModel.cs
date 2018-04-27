using System.ComponentModel.DataAnnotations;
using SAE.ShoppingMall.Identity.Dto;

namespace SAE.ShoppingMall.Identity.Models
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

        
        public static implicit operator CredentialsDto(LoginModel self)
        {
            return new CredentialsDto
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
