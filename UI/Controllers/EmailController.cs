using SRV.ViewModel;
using System.Web.Mvc;
using UI.Filters;

namespace UI.Controllers
{
    [NeedLoginFilter]
    public class EmailController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(EmailModel emailModel)
        {
            return View();
        }
    }
}
