using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeFinder.DataLayer.Models;
using RestSharp;
using RecipeFinder.DataLayer;
using RecipeFinder.DataLayer.Repositories;

namespace RecipeFinder.DevTools.Commands
{
    public class DummyRecipesCommand
    {

        private static string connString = @"Data Source=.\SQLExpress;Initial Catalog=RecipeFinder;Integrated Security=True;";

        public static void RunCommand()
        {
            UserRepository ur = new UserRepository(connString);

            User user = new User();
            user.Username = "xniko";
            user.Email = "test@test.local";
            user.Password = "123456";
            user.CreatedAt = DateTime.Now;

            ur.Create(user);
        }

        public static void RunCommandWorking()
        {
            var client = new RestClient("https://jsonplaceholder.typicode.com/");
            var request = new RestRequest("posts/23", Method.GET);
            var queryResult = client.Execute<List<Recipe>>(request).Data;

            foreach (Recipe recipe in queryResult)
            {
                Console.WriteLine("Id: " + recipe.id);
                Console.WriteLine("Title: " + recipe.title);
                Console.WriteLine("Body: " + recipe.body);
                Console.WriteLine("UserID: " + recipe.userId);

                Console.WriteLine("-----------------------------------------------\n\n");
            }
        }
    }
}
