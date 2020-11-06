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
            SetFlash(Models.FlashMessageType.Success, "Dette er en Flash-Message. Denne er blevet sat i HomeController/index metoden. Disse FlashMessages bliver renderet lige før view'et. Men bliver sat i Viewed eller Controller metoden. Forsvinder når brugeren har set dem én gang.");
            //SetFlash(Models.FlashMessageType.Warning, "Flash Message: Warning");
            //SetFlash(Models.FlashMessageType.Danger, "Flash Message: Danger/Error");

            return View();
        }
    }
}