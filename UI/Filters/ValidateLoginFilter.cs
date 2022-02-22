using Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        }
    }
}