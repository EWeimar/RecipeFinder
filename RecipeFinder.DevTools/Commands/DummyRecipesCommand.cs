using System;
using RecipeFinder.DataLayer.Models;
using RestSharp;
using RecipeFinder.DataLayer.Repositories;
using RecipeFinder.DevTools.Commands.MealDB;

namespace RecipeFinder.DevTools.Commands
{
    public class DummyRecipesCommand
    {
        private static string connString = @"Data Source=.\SQLExpress;Initial Catalog=RecipeFinder;Integrated Security=True;";

        public static void RunCommand()
        {
            insertDummyIngredients();
        }

        private static void insertDummyIngredients()
        {
            var client = new RestClient("https://www.themealdb.com/api/json/v1/1/");
            var request = new RestRequest("list.php?i=list", Method.GET);
            var queryResult = client.Execute<MealDBIngredientsList>(request).Data;

            IngredientRepository ingredientRepository = new IngredientRepository(connString);

            foreach (MealDBIngredient mealDbIngredient in queryResult.meals)
            {
                Ingredient newIngredientEntity = new Ingredient();
                newIngredientEntity.Name = mealDbIngredient.strIngredient;

                ingredientRepository.Create(newIngredientEntity);
            }
        }
    }
}
