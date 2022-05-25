using Autofac;
using Autofac.Integration.Mvc;
using SRV.ProductionService;
using System;
using System.Reflection;
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

            // 注册IOC容器
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterAssemblyTypes(typeof(BaseService).Assembly).AsImplementedInterfaces();
            // 设置Autofac为依赖解析器
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

#if !DEBUG
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
                        Response.WriteFile("~/Views/Shared/NotFound.cshtml");
                        Server.ClearError();
                        return;
                    }
                }
                // 根据需求进行日志记录，或者处理其他业务流程
                Response.StatusCode = 500;
                Response.WriteFile("~/Views/Shared/Error.cshtml");
                Server.ClearError();
            }
        }
#endif
    }
}
