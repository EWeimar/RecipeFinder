using RecipeFinder.WebClient.ApiHelpers;
using RecipeFinder.WebClient.Models;
using System.Web.Mvc;

namespace RecipeFinder.WebClient.Controllers
{
    public class RecipeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Message = "Create Recipe";

            return View();
        }

        [HttpPost]
        public ActionResult SubmitCreate()
        {
            return Redirect(Url.Action("Index", "Recipe"));
        }

        [HttpGet]
        public ActionResult Update(string slug)
        {
            ViewBag.slug = slug;
            return View();

        }

        [HttpPost]
        public ActionResult SubmitUpdate()
        {
            return Redirect(Url.Action("Index", "Recipe"));
        }

        
    }
}