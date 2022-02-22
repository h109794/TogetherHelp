using Global;
using SRV.ProductionService;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Filters;

namespace UI.Controllers
{
    [ModelValidationFilter]
    public class RegisterController : Controller
    {
        public ActionResult Index()
        {
            if (ViewData[Key.HasLogin] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(RegisterModel model)
        {
            IRegisterService registerService = new RegisterService();

            if (!registerService.ValidateInviterIsExists(model.Inviter, out string invitationCode))
            {
                ModelState.AddModelError(nameof(model.Inviter), "* 邀请人不存在");
            }
            else if (invitationCode != model.InvitationCode)
            {
                ModelState.AddModelError(nameof(model.InvitationCode), "* 邀请码错误");
            }// else nothing

            if (registerService.ValidateUserIsExists(model.Username))
            {
                ModelState.AddModelError(nameof(model.Username), "* 用户名已存在");
            }// else nothing

            if (!ModelState.IsValid)
            {
                return View();
            }

            int id = registerService.Register(model);

            HttpCookie cookie = new HttpCookie(Key.LoginInfo);
            cookie.Values.Add(Key.Id, id.ToString());
            cookie.Values.Add(Key.Pwd, Utility.MD5Encrypt(model.Password));
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "Home");
        }
    }
}
