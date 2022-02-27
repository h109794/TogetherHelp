using Global;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class SharedController : Controller
    {
        public ActionResult GenerateCaptcha()
        {
            MemoryStream captchaStream = Utility.Captcha.GenerateCaptcha(out string code);
            Session.Add(Key.Captcha, code);

            return File(captchaStream, "image/jpge");
        }
    }
}