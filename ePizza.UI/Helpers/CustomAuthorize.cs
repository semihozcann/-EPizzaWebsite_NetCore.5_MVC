using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ePizza.UI.Helpers
{
    public class CustomAuthorize : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                if (!context.HttpContext.User.IsInRole(Roles))
                {
                    context.Result = new RedirectToActionResult("Unauthorize", "Account", new { area = "" });
                }
                else
                {
                    context.Result = new RedirectToActionResult("Login", "Account", new { area = "" });
                }

            }
        }
    }
}
