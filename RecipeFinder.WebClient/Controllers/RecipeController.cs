using RecipeFinder.DTO;
using RecipeFinder.WebClient.ApiHelpers;
using RecipeFinder.WebClient.Models;
using System.Collections.Generic;
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
        public ActionResult SubmitCreate(RecipeModel rm)
        {
            RecipeCaller rc = new RecipeCaller("https://localhost:44320/api");

            RecipeDTO recipeToBeCreated = new RecipeDTO();
            recipeToBeCreated.Title = rm.Title;
            recipeToBeCreated.User = new UserDTO() { Id = 1 };
            recipeToBeCreated.Instruction = rm.Instructions;
            recipeToBeCreated.IngredientLines = new List<IngredientLineDTO>();
            recipeToBeCreated.Images = new List<ImageDTO>();

            if (rm.IngredientLines != null)
            {
                foreach (var item in rm.IngredientLines)
                {
                    IngredientLineDTO lineDTO = new IngredientLineDTO();
                    lineDTO.Ingredient = new IngredientDTO() { Name = item.Ingredient.Name };
                    lineDTO.Amount = item.Amount;
                    lineDTO.MeasureUnitText = item.MeasureUnit;

                    recipeToBeCreated.IngredientLines.Add(lineDTO);
                }
            }
            if (rm.Images != null)
            {
                foreach (var item in rm.Images)
                {
                    ImageDTO image = new ImageDTO();
                    image.FileName = item.FileName;

                    recipeToBeCreated.Images.Add(image);
                }
            }

            RFApiResult response = rc.CreateRecipe(recipeToBeCreated);

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