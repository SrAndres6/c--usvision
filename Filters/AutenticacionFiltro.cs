using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace proyecto_c_.Filters
{
    public class AutenticacionFiltro : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var usuario = context.HttpContext.Session.GetString("UsuarioLogueado");
            var controller = context.RouteData.Values["controller"]?.ToString();

            if (string.IsNullOrEmpty(usuario) && controller != "Login")
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
            }

            base.OnActionExecuting(context);
        }
    }
}
