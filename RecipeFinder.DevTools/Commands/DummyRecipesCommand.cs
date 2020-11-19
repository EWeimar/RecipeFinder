using System;
using RecipeFinder.DataLayer.Models;
using RestSharp;
using RecipeFinder.DataLayer.Repositories;
using RecipeFinder.DevTools.Commands.MealDB;
using RecipeFinder.DTO;
using System.Collections.Generic;
using RecipeFinder.BusinessLayer.Services;
using RecipeFinder.BusinessLayer.Interfaces;
using RecipeFinder.DataLayer;

namespace RecipeFinder.DevTools.Commands
{
    public class DummyRecipesCommand
    {
        private static string connString = @"Data Source=.\SQLExpress;Initial Catalog=RecipeFinderDB;Integrated Security=True;";

        public static void RunCommand()
        {

            EnLilleTest();

            //InsertDummyIngredients();
            //GenerateDummyUser();
            //GenerateRandomDummyRecipe();
        }

        private static void EnLilleTest()
        {

            RecipeService rs = new RecipeService();

            RecipeDTO obj = new RecipeDTO();
            obj.Id = 0;
            obj.User = new UserDTO() { Id = 1 };
            obj.Title = "Sandwich";
            obj.Slug = "Sandwich-Slug";
            obj.Instruction = "Lav en sandwich";
            obj.IngredientLines = new List<IngredientLineDTO>()
            {
                new IngredientLineDTO()
                {
                    Id = 0,
                    Ingredient = new IngredientDTO()
                    {
                        Id = 0,
                        Name = "Brød"
                    },
                    Amount = 1,
                    MeasureUnit = MeasureUnit.Stk
                }
            };

            obj.Images = new List<ImageDTO>()
            {
                new ImageDTO()
                {
                    Id = 0,
                    FileName = "FlotSandwich.jpg"
                },
                new ImageDTO()
                {
                    Id = 0,
                    FileName = "LækkerSandwich.png"
                }
            };

            rs.Create(obj);
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
