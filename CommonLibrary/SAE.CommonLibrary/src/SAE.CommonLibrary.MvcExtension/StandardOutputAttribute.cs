using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SAE.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAE.CommonLibrary.MvcExtension
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class StandardOutputAttribute : Attribute, IActionFilter, IOrderedFilter
    {
        public StandardOutputAttribute()
        {
            this.Order = FilterScope.Controller - 1;
        }
        public int Order { get; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null && !context.ExceptionHandled)
            {
                var exception = context.Exception as SAEException;
                if (exception != null)
                {
                    context.Result = new JsonResult(new StandardResult(exception.Code, exception.Message));
                }
                else
                {
                    context.Result = new JsonResult(new StandardResult(StatusCode.Unknown));
                }
                context.ExceptionHandled = true;
            }
            else
            {
                var jsonResult = context.Result as JsonResult;
                if (jsonResult != null && !(jsonResult.Value is StandardResult))
                {
                    jsonResult.Value = new StandardResult(jsonResult.Value);
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
