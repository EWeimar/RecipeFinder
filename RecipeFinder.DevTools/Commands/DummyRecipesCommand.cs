using System;
using RecipeFinder.DataLayer.Models;
using RestSharp;
using RecipeFinder.DataLayer.Repositories;
using RecipeFinder.DevTools.Commands.MealDB;
using RecipeFinder.DTO;

namespace RecipeFinder.DevTools.Commands
{
    public class DummyRecipesCommand
    {
        private static string connString = @"Data Source=.\SQLExpress;Initial Catalog=RecipeFinder;Integrated Security=True;";

        public static void RunCommand()
        {
            //InsertDummyIngredients();
            //GenerateDummyUser();
            GenerateRandomDummyRecipe();
        }

        private static void GenerateDummyUser()
        {
            UserRepository userRepository = new UserRepository(connString);
            User user = new User();

            user.Username = "EvilEagle";
            user.Email = "evil.eagle@mail.local";
            user.Password = "123456";
            user.IsAdmin = false;

            userRepository.Create(user);
        }

        private static void InsertDummyIngredients()
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

        private static void GenerateRandomDummyRecipe()
        {
            var client = new RestClient("https://www.themealdb.com/api/json/v1/1/");
            var request = new RestRequest("random.php", Method.GET);
            var queryResult = client.Execute<MealDBRecipeList>(request).Data;

            RecipeRepository recipeRepository = new RecipeRepository(connString);
            IngredientLineRepository ingredientLineRepository = new IngredientLineRepository(connString);

            foreach (MealDBRecipe mealDbRecipe in queryResult.meals)
            {
                Console.WriteLine("Recipe Name: " + mealDbRecipe.strMeal);

                Recipe recipe = new Recipe();
                recipe.Title = mealDbRecipe.strMeal;
                recipe.UserId = 1;
                recipe.Slug = "139232-adasd-dasdsdf-fsdsd-f";
                recipe.Instruction = mealDbRecipe.strInstructions;
                recipe.CreatedAt = DateTime.Now;

                recipeRepository.Create(recipe);

                Console.WriteLine("recipeId " + recipe.Id);


            }
        }
    }
}
