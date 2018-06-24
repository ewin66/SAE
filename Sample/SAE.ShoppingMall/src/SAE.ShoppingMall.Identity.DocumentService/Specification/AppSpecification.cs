using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using SAE.ShoppingMall.Identity.Dto;
using SAE.ShoppingMall.Infrastructure.Specification;
using SAE.ShoppingMall.Infrastructure.Specification.Expression;

namespace SAE.ShoppingMall.Identity.DocumentService.Specification
{
    public class AppSpecification : ISpecification<AppDto>
    {
        public AppSpecification(string name,string url,string id)
        {

            if (!string.IsNullOrWhiteSpace(id))
            {
                this.Expression = app => app.AppId == id;
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                this.Expression= this.Expression.Or(s => s.Name.Contains(name));
            }

            if (!string.IsNullOrWhiteSpace(url))
            {
                this.Expression = this.Expression.And(s => s.Name.Contains(url));
            }

            
        }
        public Expression<Func<AppDto, bool>> Expression
        {
            get; set;
        }
    }
}
