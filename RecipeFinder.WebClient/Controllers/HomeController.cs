using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipeFinder.WebClient.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            SetFlash(Models.FlashMessageType.Success, "flash test.");

            return View();
        }
    }
}