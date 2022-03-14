using SRV.ProductionService;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System;
using System.Web.Mvc;
using UI.Filters;
using UI.Helper;

namespace UI.Controllers
{
    [NeedLoginFilter]
    public class PersonalDataController : Controller
    {
        private readonly IPersonalDataService personalDataService;

        public PersonalDataController() => personalDataService = new PersonalDataService();

        public ActionResult Index()
        {
            PersonalDataModel personalDataModel = personalDataService.Get(CookieHelper.GetCurrentUserId());
            return View(personalDataModel);
        }

        [HttpPost]
        public ActionResult Index(PersonalDataModel personalData)
        {
            if (Convert.ToDateTime(personalData.Birthday) > DateTime.Now)
            {
                ModelState.AddModelError(nameof(personalData.Birthday), "* 生日不能大于当前日期");
                return View();
            }
            personalDataService.Save(CookieHelper.GetCurrentUserId(), personalData);
            return RedirectToAction("Index");
        }
    }
}
