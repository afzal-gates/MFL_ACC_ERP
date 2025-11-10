using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace ERP.Core
{
    // Note: ASP.NET Core uses middleware for exception handling
    // This class is kept for backwards compatibility but you should use
    // app.UseExceptionHandler() in Program.cs instead

    public class ExceptionHandler
    {
        public static async Task HandleExceptionAsync(HttpContext context, System.Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var errorMessage = new ErrorMessage
            {
                Message = exception.Message
            };

            await context.Response.WriteAsJsonAsync(errorMessage);
        }

        public virtual bool ShouldHandle(System.Exception exception)
        {
            return true;
        }
    }
}
