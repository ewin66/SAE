using SAE.ShoppingMall.Identity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.ShoppingMall.Identity.API.Models
{
    public class RegisterViewModel
    {
        public string LoginName { get; set; }
        public string Password { get; set; }

        public static implicit operator CredentialsDto(RegisterViewModel viewModel)
        {
            return new CredentialsDto
            {
                Name = viewModel.LoginName,
                Password = viewModel.Password
            };
        }
    }
}
