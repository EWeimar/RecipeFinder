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

        [HttpPut]
        [Route("api/recipe/update")]
        public async Task<HttpResponseMessage> Update([FromBody] RecipeDTO recipeDTO)
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

            if (await RecipeService.UpdateAsync(recipeDTO) != 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { message = "Recipe was succesfully updated" });
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError, new { meesage = "Something horrible went wrong." });
        }

        [HttpGet]
        [Route("api/recipe/{id}")]
        public async Task<HttpResponseMessage> Get(int? id)
        {
            if (!id.HasValue)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { meesage = "Invalid ID" });
            }

            RecipeDTO recipe = await RecipeService.GetByIdAsync(id.Value);

            if (recipe == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = "Recipe with this ID does not exist" });
            }

            if (recipe != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, recipe);
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError, new { meesage = "Something horrible went wrong." });
        }


        [HttpGet]
        [Route("api/recipe/measureunits")]
        public async Task<HttpResponseMessage> GetAllMeasureUnits()
        {
            var measureUnits = await RecipeService.GetAllMeasureUnits();

            return Request.CreateResponse(HttpStatusCode.OK, measureUnits);
        }

        [HttpGet]
        [Route("api/recipe/slug/{slug}")]
        public async Task<HttpResponseMessage> GetBySlug(string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = "Invalid Slug" });
            }

            var result = await RecipeService.FindByCondition("slug", slug);
            RecipeDTO recipe = result;

            //RecipeDTO recipe = await RecipeService.GetByIdAsync(id.Value);

            if (recipe == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = "Recipe with this Slug does not exist" });
            }

            if (recipe != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, recipe);
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError, new { meesage = "Something horrible went wrong." });
        }

        [HttpGet]
        [Route("api/recipe/get_all")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var recipes = await RecipeService.GetAllAsync();

            return Request.CreateResponse(HttpStatusCode.OK, new { recipes = recipes});
        }

    }


}
