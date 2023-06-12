using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Tera_Web.Filters
{
    public class FilterSessionValidation : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var roleID = filterContext.HttpContext.Session.GetString("roleID");
            // Verificar si el usuario tiene la variable de sesión "roleID" con el valor adecuado
            if (roleID != "1")
            {
                // Redirigir al usuario a la vista de error
                filterContext.Result = new RedirectToActionResult("Error", "Permissions", null);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
