using RecipeFinder.BusinessLayer.Interfaces;
using RecipeFinder.BusinessLayer.Services;
using RecipeFinder.DataLayer.Models;
using RecipeFinder.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;

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

        [HttpPost]
        [Route("api/recipe/create")]
        public async Task<HttpResponseMessage> Create([FromBody] RecipeDTO recipeDTO)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Empty;

                foreach (ModelState keyValuePairs in ModelState.Values)
                {
                    foreach (ModelError modelError in keyValuePairs.Errors)
                    {
                        errorMessage += " - " + modelError.ErrorMessage;
                    }
                }

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }

            if (await RecipeService.AddAsync(recipeDTO) != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { message = "Recipe was succesfully created" });
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError, new { message = "Something horrible went wrong." });
        }

    }
}
