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
        
        public void CreateRecipe(RecipeDTO recipe)
        {
            RecipeService.Create(recipe);
        }

        public RecipeDTO GetRecipe(int id)
        {
            return RecipeService.Get(id);
        }

        public void UpdateRecipe(RecipeDTO recipe)
        {
            RecipeService.Update(recipe);
        }

        public void DeleteRecipe(int id)
        {

        }
    }
}
