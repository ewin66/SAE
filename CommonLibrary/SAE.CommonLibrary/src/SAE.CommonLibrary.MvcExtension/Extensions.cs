using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SAE.CommonLibrary.MvcExtension
{
    public static class Extensions
    {
        /// <summary>
        /// 验证请求是否来自于ajax
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsAjax(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";

            return false;
        }

        /// <summary>
        /// 添加MVC验证
        /// </summary>
        /// <param name="builder"></param>
        public static IMvcBuilder AddValidation(this IMvcBuilder builder, Assembly assembly = null)
        {
            if (assembly == null)
                assembly = Assembly.GetCallingAssembly();

            builder.AddFluentValidation(config =>
            {
                config.RegisterValidatorsFromAssembly(assembly);
            });

            return builder;
        }
    }
}
