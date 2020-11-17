using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace RecipeFinder.DevTools.Commands
{
    public class DummyRecipesCommand
    {

        public static void RunCommand()
        {
            var client = new RestClient("https://jsonplaceholder.typicode.com/");
            var request = new RestRequest("posts", Method.GET);
            var queryResult = client.Execute<List<Recipe>>(request).Data;


            foreach (Recipe recipe in queryResult)
            {
                Console.WriteLine("Item Title: " + recipe.title);
            }
        }
    }
}
