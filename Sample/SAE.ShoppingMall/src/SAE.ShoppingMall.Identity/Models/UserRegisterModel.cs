using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.ShoppingMall.Identity.Models
{
    public class UserRegisterModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string RetypePassword { get; set; }
        public static implicit operator Dto.CredentialsDto(UserRegisterModel model)
        {
            return new Dto.CredentialsDto
            {
                Name = model.Name,
                Password = model.Password
            };
        }
    }
}
