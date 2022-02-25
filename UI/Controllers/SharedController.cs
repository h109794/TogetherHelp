using Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
            Bitmap captcha = Utility.Captcha.GenerateCaptcha(out string code);
            Session.Add(Key.Captcha, code);

            MemoryStream memoryStream = new MemoryStream();
            captcha.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return File(memoryStream, "image/jpge");
        }
    }
}