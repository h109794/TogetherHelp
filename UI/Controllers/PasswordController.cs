using SRV.ProductionService;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Filters;
using UI.Helper;

namespace UI.Controllers
{
    [NeedLoginFilter]
    [ModelValidationFilter]
    public class PasswordController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ChangePasswordModel pwdModel)
        {
            IPasswordService passwordService = new PasswordService();

            if (pwdModel.OriginalPwd == pwdModel.NewPwd)
            {
                ModelState.AddModelError(nameof(pwdModel.NewPwd), "* 新密码不能和原密码相同");
                return View();
            }
            if (!passwordService.ChangePassword(CookieHelper.GetCurrentUserId(), pwdModel))
            {
                ModelState.AddModelError(nameof(pwdModel.OriginalPwd), "* 原密码错误");
                return View();
            }// else password changed successfully

            return Redirect("~/Login/Logoff");
        }
    }
}
