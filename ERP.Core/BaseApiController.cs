using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace ERP.Core
{
    [Authorize]
    [ValidationFilter]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        public string UserId
        {
            get
            {
                // ASP.NET Core uses HttpContext directly (not HttpContext.Current)
                // Session needs to be configured in Program.cs/Startup
                if (HttpContext?.Session != null)
                {
                    var userId = HttpContext.Session.GetString("multiScUserId");
                    if (!string.IsNullOrEmpty(userId))
                    {
                        return userId;
                    }
                }

                return string.Empty;
            }
        }

        // Alternative: Use Claims-based authentication (recommended for ASP.NET Core)
        public string GetUserIdFromClaims()
        {
            var userId = User?.FindFirst("UserId")?.Value;
            return userId ?? string.Empty;
        }
    }
}
