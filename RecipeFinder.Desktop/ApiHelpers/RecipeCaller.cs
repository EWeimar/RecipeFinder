using RecipeFinder.DataLayer.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.Desktop.ApiHelpers
{
    //public class RecipeCaller : IRecipeCaller
    //{
    //    private RestClient client;

    //    public RecipeCaller(string baseUrl)
    //    {
    //        client = new RestClient(baseUrl);
    //    }

    //    //parameter-less constructor using default API URL from App.Config
    //    public RecipeCaller()
    //{
    //    client = new RestClient(ConfigurationManager.AppSettings["StarwarsApiURL"]);
    //}

    //    public List<Recipe> GetRecipes()
    //    {
    //        var request = new RestRequest("recipe", Method.GET);
    //        var response = client.Execute<RecipeList>(request);
    //        return response.Data.results;
    //    }

    //    public Recipe GetRecipe(int id)
    //    {
    //        var request = new RestRequest("recipe/" + id, Method.GET);
    //        var response = client.Execute<Recipe>(request);
    //        return response.Data;
    //    }

    //    public void Create(Recipe recipe)
    //    {
    //        var request = new RestRequest("recipe", Method.POST);
    //        request.AddJsonBody(recipe);
    //        var response = client.Execute(request);
    //    }

    //    public void Update(int id, Recipe recipe)
    //    {
    //        var request = new RestRequest("recipe/" + id, Method.PUT);
    //        request.AddJsonBody(recipe);
    //        client.Execute(request);
    //    }

    //    public void Delete(int id)
    //    {
    //        var request = new RestRequest("recipe/" + id, Method.DELETE);
    //        client.Execute(request);
    //    }      

    //}
}
