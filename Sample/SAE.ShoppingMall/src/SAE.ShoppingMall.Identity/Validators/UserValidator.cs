using FluentValidation;
using SAE.ShoppingMall.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.ShoppingMall.Identity.Validators
{
    public class UserValidator : AbstractValidator<UserRegisterModel>
    {
        public UserValidator()
        {
            this.RuleFor(s => s.Name)
                .NotEmpty()
                .Matches(@"^\w{3,24}$");

            this.RuleFor(s => s.Password)
                .NotEmpty()
                .Matches(@"^\w{3,24}$");
                
            this.RuleFor(s => s.RetypePassword)
                .Equal(s => s.Password);

        }
    }
}
