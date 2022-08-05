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
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UploadProfile(HttpPostedFileBase profile)
        {
            if (!profile.ContentType.Contains("image") || profile.ContentLength > 1024 * 1024 * 2)
            {
                return Json(false);
            }

            int userId = CookieHelper.GetCurrentUserId();
            personalDataService.ChangeProfile(userId, profile);
            HttpContext.Cache.Remove(Key.NavProfile + userId);
            // 指示下次请求从数据库获取头像
            Response.Cookies[Key.HasProfile].Value = "True";

            return Json(true);
        }
    }
}
