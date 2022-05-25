using System.Web.Mvc;
using System.Web.Routing;

namespace UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ArticleList",
                url: "article/page/{pageIndex}",
                defaults: new { controller = "Article", action = "Index", pageIndex = UrlParameter.Optional },
                constraints: new { pageIndex = @"\d*" }
            );

            routes.MapRoute(
               name: "MyArticles",
               url: "article/my/{pageIndex}",
               defaults: new { controller = "Article", action = "My", pageIndex = UrlParameter.Optional },
               constraints: new { pageIndex = @"\d*" }
           );

            routes.MapRoute(
                name: "ArticleSingle",
                url: "article/{articleId}",
                defaults: new { Controller = "Article", action = "Single" },
                constraints: new { articleId = @"\d*" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
