using Global;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System;
using System.Web;
using System.Web.Mvc;
using UI.Filters;
using UI.Helper;

namespace UI.Controllers
{
    [NeedLoginFilter]
    public class PersonalDataController : Controller
    {
        private readonly IPersonalDataService personalDataService;

        public PersonalDataController(IPersonalDataService personalDataService) => this.personalDataService = personalDataService;

        public ActionResult Index()
        {
            var errorMessages = TempData[Key.ErrorMessages];
            if (errorMessages != null)
            {
                ModelState.Merge(errorMessages as ModelStateDictionary);
            }

            PersonalDataModel personalDataModel = personalDataService.Get(CookieHelper.GetCurrentUserId());
            return View(personalDataModel);
        }

        [HttpPost]
        public ActionResult Index(PersonalDataModel personalData)
        {
            if (personalDataService.ValidateNicknameExists(CookieHelper.GetCurrentUserId(), personalData.Nickname))
            {
                ModelState.AddModelError(nameof(personalData.Nickname), "* 该昵称已存在");
            }
            if (Convert.ToDateTime(personalData.Birthday) > DateTime.Now)
            {
                ModelState.AddModelError(nameof(personalData.Birthday), "* 生日不能大于当前日期");
            }
            if (!ModelState.IsValid)
            {
                TempData.Add(Key.ErrorMessages, ModelState);
                return RedirectToAction("Index");
            }

            personalDataService.Save(CookieHelper.GetCurrentUserId(), personalData);
            Response.Cookies[Key.Nickname].Value = HttpUtility.UrlEncode(personalData.Nickname);

            return RedirectToAction("Index");
        }
    }
}
