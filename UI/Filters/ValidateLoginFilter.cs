using Global;
using System.Web.Mvc;

namespace UI.Filters
{
    public class ValidateLoginFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData[Key.HasLogin] = filterContext.HttpContext.Request.Cookies[Key.LoginInfo];
            filterContext.Controller.ViewData[Key.Nickname] = filterContext.HttpContext.Request.Cookies[Key.Nickname]?.Value;
        }
    }
}