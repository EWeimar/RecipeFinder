using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeFinder.DevTools.Commands.DummyRecipes.ApiHelpers;

namespace RecipeFinder.DevTools.Commands
{
    public class DummyRecipesCommand
    {
        public static void RunCommand()
        {
            Console.WriteLine("Hello from DummyRecipes");

            // get a list of film from API
            RecipeCaller recipesCaller = new RecipeCaller("https://jsonplaceholder.typicode.com/");
            var listOfRecipes = recipesCaller.GetRecipes();
            foreach (var recipe in listOfRecipes)
            {
                Console.WriteLine("name: " + recipe.title);
                Console.WriteLine("body: " + recipe.body);
            }
        }
    }
}
