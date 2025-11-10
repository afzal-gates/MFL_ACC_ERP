using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ERP.Core
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ValidationFilterAttribute : ActionFilterAttribute
    {
        private static readonly Dictionary<string, List<ParameterInfo>> CachedParameters = new Dictionary<string, List<ParameterInfo>>();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments == null || context.ActionArguments.Count == 0)
            {
                return;
            }

            string actionName = context.ActionDescriptor.DisplayName ?? string.Empty;
            Type controllerType = context.Controller.GetType();
            MethodInfo? methodInfo = controllerType.GetMethod(context.ActionDescriptor.DisplayName?.Split('.').Last() ?? string.Empty);

            if (methodInfo == null)
            {
                return;
            }

            List<ParameterInfo>? parameters;
            // get parameters from cache
            CachedParameters.TryGetValue(actionName, out parameters);

            if (parameters == null)
            {
                // parameters not cached yet, need to get from method
                parameters = methodInfo.GetParameters().Where(x => !x.IsOut).ToList();

                // cache parameters
                CachedParameters.Add(actionName, parameters);
            }

            ValidationContext validationContext = new ValidationContext(this);
            List<ValidationResult> errors = new List<ValidationResult>();

            foreach (var parameter in parameters)
            {
                string name = parameter.Name ?? string.Empty;

                if (!context.ActionArguments.TryGetValue(name, out object? value))
                {
                    continue;
                }

                // validate parameter
                validationContext.DisplayName = name;
                List<ValidationAttribute> validations = parameter.GetCustomAttributes<ValidationAttribute>().ToList();

                if (validations.Any())
                {
                    Validator.TryValidateValue(value, validationContext, errors, validations);
                }
            }

            string parameterMessage = string.Empty;
            if (errors.Any())
            {
                parameterMessage = string.Join(Environment.NewLine, errors.Select(x => x.ErrorMessage));
            }

            if (!string.IsNullOrWhiteSpace(parameterMessage))
            {
                ErrorMessage errorMessage = new ErrorMessage
                {
                    Message = parameterMessage
                };

                context.Result = new BadRequestObjectResult(errorMessage);
            }
        }
    }
}
