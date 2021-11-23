using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TTGS.Shared.Constants;

namespace TTGS.Web.Security
{
    public class CustomAuthorization : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string[] _roles;
        public CustomAuthorization(params string[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            var isInRole = false;
            foreach (var role in _roles)
            {
                isInRole = user.IsInRole(role);
                if (isInRole || user.IsInRole(UserRoleConstants.Admin))
                    break;
            }

            if (!user.Identity.IsAuthenticated || !isInRole)
            {
                context.Result = new UnauthorizedObjectResult("Unauthorized");
                return;
            }

        }
    }
}
