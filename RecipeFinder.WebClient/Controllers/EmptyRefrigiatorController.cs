using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipeFinder.WebClient.Controllers
{
    public class EmptyRefrigiatorController : Controller
    {
        // GET: EmptyRefrigiator
        public ActionResult Index()
        {
            return View();
        }
    }
}