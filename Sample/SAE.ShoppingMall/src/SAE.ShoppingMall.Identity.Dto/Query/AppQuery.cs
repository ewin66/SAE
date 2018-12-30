using SAE.CommonLibrary.Common;
using SAE.ShoppingMall.Infrastructure;
using SAE.ShoppingMall.Infrastructure.Specification;
using SAE.ShoppingMall.Infrastructure.Specification.Expression;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SAE.ShoppingMall.Identity.Dto.Query
{
    public class AppQuery : Paging,ISpecification<AppDto>
    {
        public string AppId { get; set; }
        public string Name { get; set; }

        Expression<Func<AppDto, bool>> ISpecification<AppDto>.Expression
        {
            get
            {
                
                Expression<Func<AppDto, bool>> expression=null;
                if (!AppId.IsNullOrWhiteSpace())
                {
                    expression.And(s => s.AppId == this.AppId);
                }

                if (!Name.IsNullOrWhiteSpace())
                {
                    expression.And(s => s.Name.Contains(this.Name));
                }

                return expression;
            }
        }
    }
}
