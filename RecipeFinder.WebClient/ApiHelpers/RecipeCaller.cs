using RecipeFinder.DataLayer.Models;
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

        public RFApiResult CreateRecipe(Recipe recipe)
        {
            var request = new RestRequest("/recipe/create", Method.POST);

            request.AddJsonBody(recipe);

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
    }
}
