using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SAE.ShoppingMall.Identity.DocumentService.Specification;
using SAE.ShoppingMall.Identity.Dto;
using SAE.ShoppingMall.Infrastructure.Specification;

namespace SAE.ShoppingMall.Identity.API.Models
{
    public class AppQueryModel : PagingModel
    {
        public AppQueryModel()
        {
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 请求地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// appid
        /// </summary>

        public string AppId { get; set; }

        public static implicit operator AppSpecification(AppQueryModel appQuery)
        {
            return new AppSpecification(appQuery.Name, appQuery.Url, appQuery.AppId);
        }
    }
}
