using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception error = Server.GetLastError();

            if (error != null)
            {
                // 对404做额外处理，其他错误全部当成500服务器错误
                if (error is HttpException httpError)
                {
                    int httpCode = httpError.GetHttpCode();
                    if (httpCode == 400 || httpCode == 404)
                    {
                        Response.StatusCode = 404;
                        Response.WriteFile("~/Views/Shared/NotFound.html");
                        Server.ClearError();
                        return;
                    }
                }
                // 根据需求进行日志记录，或者处理其他业务流程
                Response.StatusCode = 500;
                Response.WriteFile("~/Views/Shared/Error.html");
                Server.ClearError();
            }
        }
    }
}
