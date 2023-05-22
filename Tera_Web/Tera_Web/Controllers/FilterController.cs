using Microsoft.AspNetCore.Mvc.Filters;

namespace Tera_Web.Controllers
{
    public class FiltroPersonalizadoSesion : ActionFilterAttribute
    {
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    if (filterContext.HttpContext.Session.Count == 0)
        //    {
        //        filterContext.Result = new RedirectToRouteResult(new
        //        {
        //            controller = "Home",
        //            action = "Index"
        //        });
        //    }

        //    base.OnActionExecuting(filterContext);
        //}

    }
}
