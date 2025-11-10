using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ERP.Core
{
    [Authorize]
    [ValidationFilter]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Gets the authenticated user's ID from JWT claims
        /// </summary>
        public long UserId
        {
            get
            {
                var userIdClaim = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (long.TryParse(userIdClaim, out long userId))
                {
                    return userId;
                }
                return 0;
            }
        }

        /// <summary>
        /// Gets the authenticated user's name from JWT claims
        /// </summary>
        public string UserName => User?.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;

        /// <summary>
        /// Gets the authenticated user's email from JWT claims
        /// </summary>
        public string UserEmail => User?.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;

        /// <summary>
        /// Gets all roles assigned to the authenticated user from JWT claims
        /// </summary>
        public List<string> UserRoles
        {
            get
            {
                return User?.FindAll(ClaimTypes.Role)
                    .Select(c => c.Value)
                    .ToList() ?? new List<string>();
            }
        }

        /// <summary>
        /// Checks if the authenticated user has a specific role
        /// </summary>
        public bool HasRole(string roleName)
        {
            return User?.IsInRole(roleName) ?? false;
        }
    }
}
