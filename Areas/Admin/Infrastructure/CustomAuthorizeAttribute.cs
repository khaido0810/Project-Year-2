using Project_Year_2.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Project_Year_2.Areas.Admin.Infrastructure
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedroles;
        public CustomAuthorizeAttribute(params string[] roles)
        {
            this.allowedroles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            var userId = Convert.ToString(httpContext.Session["User"]);
            if (!string.IsNullOrEmpty(userId))
                using (var context = new QuanLyNhaHangDBContext())
                {
                    var userRole = context.Accounts.FirstOrDefault(x=>x.UserName == userId);
                    foreach (var role in allowedroles)
                    {
                        if (role == userRole.Role) return true;
                    }
                }


            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    {"controller", "Error" },
                    { "action", "UnAuthorzied" }
               });
        }
    }
}