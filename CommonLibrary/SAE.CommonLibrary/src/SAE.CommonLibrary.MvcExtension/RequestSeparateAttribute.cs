using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SAE.CommonLibrary.MvcExtension
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =false,Inherited =false)]
    public class RequestSeparateAttribute:Attribute,IActionFilter
    {
        public string HttpMethod { get; set; } = "get";
        public RequestSeparateAttribute()
        {

        }


        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Method.Equals(this.HttpMethod, StringComparison.OrdinalIgnoreCase))
            {
                object model = null;
                if (context.ActionArguments.Count == 1)
                {
                    model = context.ActionArguments.Values.First();
                }else if (context.ActionArguments.Count > 1)
                {
                    model = context.ActionArguments;
                }
                
                var controller= context.Controller as Controller;

                var result = new ViewResult
                {
                    ViewData = controller?.ViewData
                };

                result.ViewData.Model = model;

                context.Result = result;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
