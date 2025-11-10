using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ERP.Core
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ModelValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid == false)
            {
                var errors = new List<string>();
                foreach (var modelStateVal in context.ModelState.Values.Select(d => d.Errors))
                {
                    errors.AddRange(modelStateVal.Select(error => error.ErrorMessage ?? string.Empty));
                }

                ErrorMessage errorMessage = new ErrorMessage
                {
                    Message = string.Join(Environment.NewLine, errors)
                };

                if (!string.IsNullOrWhiteSpace(errorMessage.Message))
                {
                    context.Result = new BadRequestObjectResult(errorMessage);
                }
            }
        }
    }
}
