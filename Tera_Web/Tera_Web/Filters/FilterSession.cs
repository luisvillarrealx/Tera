﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Tera_Web.Filters
{
    public class FilterSession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;

            if (!session.Keys.Any())
            {
                filterContext.Result = new RedirectToRouteResult(new
                {
                    controller = "Auth",
                    action = "Login"
                });
            }

            base.OnActionExecuting(filterContext);
        }

    }
}
