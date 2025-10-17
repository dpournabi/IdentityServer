using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nsp.Common;

namespace IdentityServer.Infrastructure.Common
{
    public class ClaimRequirementFilter : Attribute, IAuthorizationFilter
    {
        public string ClaimValueCheck { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAdmin = context.HttpContext.User.Claims.Any(x => x.Value is AssessorsManager.Root or AssessorsManager.Administrator);
            if (!isAdmin)
            {
                bool hasClaim = context.HttpContext.User.Claims.Any(c => c.Value == ClaimValueCheck);
                if (!hasClaim)
                {
                    context.Result = new UnauthorizedResult();
                    context.Result = new StatusCodeResult(401);
                }
            }

        }
    }
}
