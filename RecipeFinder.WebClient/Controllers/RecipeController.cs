using RecipeFinder.DTO;
using RecipeFinder.WebClient.ApiHelpers;
using RecipeFinder.WebClient.Models;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace RecipeFinder.WebClient.Controllers
{
    public class RecipeController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("recipe/view/{id}")]
        public ActionResult ViewRecipe(string id)
        {
            RecipeCaller rc = new RecipeCaller("https://localhost:44320/api");

            RecipeModel recipe = rc.FindBySlug(id);

            if (recipe.StatusCode == HttpStatusCode.OK)
            {
                ViewBag.recipe = recipe;
                return View();
            }

            return HttpNotFound();
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
            recipeToBeCreated.Instruction = rm.Instruction;
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

            if (response.StatusCode == HttpStatusCode.OK)
            {
                SetFlash(FlashMessageType.Success, response.Message);
            } else {
                SetFlash(FlashMessageType.Danger, response.Message);
            }

            return Redirect(Url.Action("Index", "Home"));
        }

        [HttpGet]
        [Route("recipe/update/{id}")]
        public ActionResult Update(string id)
        {
            RecipeCaller rc = new RecipeCaller("https://localhost:44320/api");

            RecipeModel recipe = rc.FindBySlug(id);

            if (recipe.StatusCode == HttpStatusCode.OK)
            {
                ViewBag.recipe = recipe;

                return View();
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult SubmitUpdate()
        {
            return Redirect(Url.Action("Index", "Recipe"));
        }

    }
}