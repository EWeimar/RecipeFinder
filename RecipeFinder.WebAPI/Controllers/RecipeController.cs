using RecipeFinder.BusinessLayer.Interfaces;
using RecipeFinder.BusinessLayer.Services;
using RecipeFinder.DataLayer.Models;
using RecipeFinder.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecipeFinder.WebAPI.Controllers
{
    public class RecipeController : ApiController
    {
        private IRecipeService RecipeService;
        public RecipeController()
        {
            RecipeService = new RecipeService();
        }
        public List<IngredientLine> GetRecipes()
        {
            return new List<IngredientLine>()
            {
                new IngredientLine()
                {
                    //Name = "Pølser",
                    //Description = "Dejlige store pølser",
                    //Rating = 5,
                    MeasureUnit = DataLayer.Models.MeasureUnit.None

                }
            };
        }

        public RecipeDTO GetRecipe(int id)
        {
            return RecipeService.Get(id);
        }
    }
}
