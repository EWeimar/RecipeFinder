using System.Collections.Generic;
using System.Configuration;
using System.Net;
using RecipeFinder.DataLayer.Models;
using RestSharp;

namespace RecipeFinder.BusinessLayer.Commands.ApiHelpers
{
    public class DummyRecipeCaller : IDummyRecipesCaller
    {
        private RestClient client;
        public DummyRecipeCaller(string baseUrl)
        {
            client = new RestClient(baseUrl);
        }

        // parameter-less constructor using default API URL from App.Config
        public DummyRecipeCaller()
        {
            client = new RestClient("http://www.madopskrifter.nu/webservices/iphone/iphoneclientservice.svc/GetPopularRecipes/0");
        }

        public List<Recipe> GetRecipes()
        {
            var request = new RestRequest("films", Method.GET);
            var response = client.Execute<RecipeList>(request);
            return response.Data.results;
        }

        public Recipe GetRecipe(int id)
        {
            var request = new RestRequest("films/" + id, Method.GET);
            var response = client.Execute<Recipe>(request);
            return response.Data;
        }

        public void Create(Recipe Recipe)
        {
            var request = new RestRequest("films", Method.POST);
            request.AddJsonBody(Recipe);
            var response = client.Execute(request);
        }

        public void Update(int id, Recipe Recipe)
        {
            var request = new RestRequest("films/" + id, Method.PUT);
            request.AddJsonBody(Recipe);
            client.Execute(request);
        }

        public void Delete(int id)
        {
            var request = new RestRequest("films/" + id, Method.DELETE);
            client.Execute(request);
        }
    }
}