using RecipeFinder.DTO;
using RecipeFinder.WebClient.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeFinder.WebClient.ApiHelpers
{
    public class RecipeCaller : IRecipeCaller
    {
        private RestClient client;
        public RecipeCaller(string baseUrl)
        {
            client = new RestClient(baseUrl);
        }

        public RFApiResult CreateRecipe(RecipeDTO recipeDTO)
        {
            var request = new RestRequest("/recipe/create", Method.POST);

            request.AddJsonBody(recipeDTO);

            IRestResponse<RFApiResult> response = client.Execute<RFApiResult>(request);

            response.Data.StatusCode = response.StatusCode;

            if (response.IsSuccessful)
            {
                response.Data.Success = true;
            }
            else
            {
                response.Data.Success = false;
            }

            return response.Data;
        }

        public RFApiResult UpdateRecipe(RecipeDTO recipeDTO)
        {
            var request = new RestRequest("/recipe/update", Method.PUT);

            request.AddJsonBody(recipeDTO);

            IRestResponse<RFApiResult> response = client.Execute<RFApiResult>(request);

            response.Data.StatusCode = response.StatusCode;

            if (response.IsSuccessful)
            {
                response.Data.Success = true;
            }
            else
            {
                response.Data.Success = false;
            }

            return response.Data;
        }
        public RecipeList GetAll()
        {
            var request = new RestRequest("/recipe/get_all", Method.GET);

            IRestResponse<RecipeList> response = client.Execute<RecipeList>(request);
            //response.Data.StatusCode = response.StatusCode;

           return response.Data;
        }
    }
}
