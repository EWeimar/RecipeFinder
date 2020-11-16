using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace RecipeFinder.DevTools.Commands.DummyRecipes.ApiHelpers
{
    class RecipeCaller : IRecipeCaller
    {
        private RestClient client;
        private String baseUrl;

        public RecipeCaller(string baseUrl)
        {
            
            client = new RestClient(baseUrl);

            this.baseUrl = baseUrl;
        }

        // parameter-less constructor using default API URL from App.Config
        // ..... other construct

        public List<Recipe> GetRecipes()
        {
            Console.WriteLine("endpoint url: " + baseUrl + "posts");
            var request = new RestRequest("posts", Method.GET);

            //request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

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
