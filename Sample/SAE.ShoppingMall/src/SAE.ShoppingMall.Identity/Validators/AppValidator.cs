using FluentValidation;
using SAE.ShoppingMall.Identity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAE.ShoppingMall.Identity.Validations
{
    public class AppValidator:AbstractValidator<AppDto>
    {
        public AppValidator()
        {
            this.RuleFor(s => s.Name)
                .NotEmpty()
                .Length(2, 24);

            this.RuleFor(s => s.Signin)
                .Matches(@"^(https?):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&:/~\+#]*[\w\-\@?^=%&/~\+#])?$")
                .MaximumLength(1024);

            this.RuleFor(s => s.Signout)
                .Matches(@"^(https?):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&:/~\+#]*[\w\-\@?^=%&/~\+#])?$")
                .MaximumLength(1024);
        }
    }
}
