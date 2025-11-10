using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Core
{
    // In ASP.NET Core, use ObjectResult or specific result types instead of IHttpActionResult
    public class GlobalException : ObjectResult
    {
        public GlobalException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(new ErrorMessage { Message = message })
        {
            StatusCode = (int)statusCode;
        }

        public GlobalException(ErrorMessage errorMessage, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(errorMessage)
        {
            StatusCode = (int)statusCode;
        }
    }
}
