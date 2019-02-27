using SAE.ShoppingMall.Infrastructure;
using SAE.ShoppingMall.Infrastructure.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using SAE.CommonLibrary.Common;
using SAE.ShoppingMall.Infrastructure.Specification.Expression;

namespace SAE.ShoppingMall.Identity.Dto.Query
{
    public class UserQuery : Paging, ISpecification<UserDto>
    {
        public string Name { get; set; }
        public Expression<Func<UserDto, bool>> Expression{
            get
            {
                Expression<Func<UserDto, bool>> expression = null;
                if (!this.Name.IsNullOrWhiteSpace())
                {
                    expression.And(s=>s.Name.Contains(this.Name));
                }
                return expression;
            }
        }
    }
}
