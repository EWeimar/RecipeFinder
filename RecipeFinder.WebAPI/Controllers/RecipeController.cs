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
    public class RecipeController : ApiBaseController
    {
        private IRecipeService RecipeService;

        public IngredientLineDTO IngredientLines { get; private set; }

        public RecipeController()
        {
            RecipeService = new RecipeService();
        }
    }
}
