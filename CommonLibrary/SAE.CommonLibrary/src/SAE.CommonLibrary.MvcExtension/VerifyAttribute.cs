using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace SAE.CommonLibrary.MvcExtension
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class VerifyAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var modelError = context.ModelState.Values.First(s => s.Errors.Any()).Errors.First();
                IActionResult result;
                if (context.HttpContext.Request.IsAjax())
                {
                    result = new JsonResult(modelError.ErrorMessage);
                }
                else
                {
                    var controller= (context.Controller as Controller);
                    result = new ViewResult
                    {
                        ViewData = controller.ViewData,
                        TempData= controller.TempData,
                    };
                }
                context.Result = result;
            }
        }
    }
}
