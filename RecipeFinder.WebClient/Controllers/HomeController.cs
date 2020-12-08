using RecipeFinder.WebClient.ApiHelpers;
using RecipeFinder.WebClient.Models;
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
            //SetFlash(Models.FlashMessageType.Danger, "Hov hov mester.");

            ViewBag.recipes = GetAllRecipes();
            return View();
        }
        private List<RecipeModel> GetAllRecipes()
        {
            RecipeCaller rc = new RecipeCaller("https://localhost:44320/api");
            return rc.GetAll().recipes;
        }
    }
}