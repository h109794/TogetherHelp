using Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Filters
{
    public class NeedLoginFilterAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.Controller.ViewData[Key.HasLogin] is null)
            {
                // 退出登录验证时不添加Cookie
                if (filterContext.HttpContext.Request.QueryString.ToString() != "logoff")
                {
                    filterContext.HttpContext.Response.Cookies.Add(new HttpCookie(Key.TargetPageURL, filterContext.HttpContext.Request.Url.ToString()));
                }
                filterContext.Result = new RedirectResult("~/Login/Index?");
            }
        }
    }
}
