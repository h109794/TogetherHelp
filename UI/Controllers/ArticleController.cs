using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Filters;

namespace UI.Controllers
{
    public class ArticleController : Controller
    {
        [NeedLoginFilter]
        public ActionResult Index()
        {
            return View();
        }
    }
}