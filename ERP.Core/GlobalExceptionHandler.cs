using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace ERP.Core
{
    /// <summary>
    /// Global exception handler for ASP.NET Core
    /// Implements IExceptionHandler interface (available in .NET 8+)
    /// </summary>
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string message = "Internal exception has occurred. Please contact administrator.";

            try
            {
                // Try to map exception HResult to HTTP status code
                if (exception.HResult > 1 && exception.HResult < 600)
                {
                    statusCode = (HttpStatusCode)exception.HResult;
                }
            }
            catch
            {
                statusCode = HttpStatusCode.InternalServerError;
            }

            if (!string.IsNullOrWhiteSpace(exception.Message))
            {
                message = exception.Message;
            }

            // Set response status code
            httpContext.Response.StatusCode = (int)statusCode;
            httpContext.Response.ContentType = "application/json";

            // Create error response
            var errorResponse = new ErrorMessage
            {
                Message = message
            };

            // Write response
            await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);

            // Return true to indicate that the exception has been handled
            return true;
        }
    }
}
