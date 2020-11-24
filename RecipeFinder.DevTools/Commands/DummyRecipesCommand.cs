using System;
using RecipeFinder.DataLayer.Models;
using RestSharp;
using RecipeFinder.DataLayer.Repositories;
using RecipeFinder.DevTools.Commands.MealDB;
using RecipeFinder.DTO;
using System.Collections.Generic;
using RecipeFinder.BusinessLayer.Services;
using RecipeFinder.BusinessLayer.Lib;
using System.Linq;
using System.Threading;

namespace RecipeFinder.DevTools.Commands
{

    public class DummyRecipesCommand
    {
        private static string connString = @"Data Source=.\SQLExpress;Initial Catalog=RecipeFinderDB;Integrated Security=True;";
        private static List<string> testUsernames = new List<string>();

        public static void RunCommand()
        {
            Console.WriteLine("How many dummy recipes do you desire?:");
            var desiredRecipesCount = Console.ReadLine();

            try
            {
                int result = Int32.Parse(desiredRecipesCount);

                Console.WriteLine("Starting seed..... Wait....");
                GenerateDummyUsers();
                Thread.Sleep(2000);

                for (int i = 1; i <= result; i++)
                {
                    GenerateRandomDummyRecipe();
                    Thread.Sleep(500);
                }

                Console.WriteLine("Seed Complete");
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse '{desiredRecipesCount}'");
            }
        }

        private static void GenerateRandomDummyRecipe()
        {
            var client = new RestClient("https://www.themealdb.com/api/json/v1/1/");
            var request = new RestRequest("random.php", Method.GET);
            var queryResult = client.Execute<MealDBRecipeList>(request).Data;

            RecipeRepository recipeRepository = new RecipeRepository(connString);
            IngredientLineRepository ingredientLineRepository = new IngredientLineRepository(connString);
            UserRepository userRepository = new UserRepository(connString);

            foreach (MealDBRecipe mealDbRecipe in queryResult.meals)
            {
                IEnumerable<Recipe> existingRecipeWithSameName = recipeRepository.GetAll(nameof(Recipe.Title), mealDbRecipe.strMeal);

                // if it is already existing, skip
                if (existingRecipeWithSameName.Any())
                {
                    continue;
                }

                RecipeService rs = new RecipeService();

                string randomUserName = testUsernames[new Random().Next(0, testUsernames.Count - 1)];
                User randomUser = userRepository.GetAll("username", randomUserName).FirstOrDefault();

                RecipeDTO obj = new RecipeDTO()
                {
                    Id = 0,
                    User = new UserDTO() { Id = randomUser.Id },
                    Title = mealDbRecipe.strMeal,
                    Slug = SlugHelper.GenerateSlug(mealDbRecipe.strMeal),
                    Instruction = mealDbRecipe.strInstructions
                };
                
                obj.IngredientLines = new List<IngredientLineDTO>();
                obj.Images = new List<ImageDTO>();

                for (int i = 1; i <= 20; i++)
                {
                    if (mealDbRecipe.GetType().GetProperties().Where(p => p.Name.Equals("strIngredient" + i)).Any())
                    {
                        var ingredientStr = mealDbRecipe.GetType().GetProperty("strIngredient" + i);

                        if (ingredientStr == null)
                        {
                            continue;
                        }

                        if (ingredientStr != null)
                        {
                            try
                            {
                                if (ingredientStr.GetValue(mealDbRecipe, null) == null)
                                {
                                    continue;
                                }

                                string ingredientStrValue = ingredientStr.GetValue(mealDbRecipe, null).ToString();

                                if (!string.IsNullOrEmpty(ingredientStrValue))
                                {
                                    obj.IngredientLines.Add(new IngredientLineDTO()
                                    {
                                        Id = 0,
                                        Ingredient = new IngredientDTO()
                                        {
                                            Id = 0,
                                            Name = ingredientStrValue
                                        },
                                        Amount = 1,
                                        MeasureUnit = (MeasureUnit)new Random().Next(Enum.GetNames(typeof(MeasureUnit)).Length)
                                    });
                                }

                            } catch (Exception e)
                            {
                                Console.WriteLine("An exception was thrown: " + e.Message + "\nStack Trace:\n" + e.StackTrace + "\n");
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(mealDbRecipe.strMealThumb))
                {
                    obj.Images.Add(new ImageDTO()
                    {
                        Id = 0,
                        FileName = mealDbRecipe.strMealThumb
                    });
                }

                rs.Create(obj);
            }
        }

        private static void GenerateDummyUsers()
        {
            GenerateOneUser("EvilEagle", "evil.eagle@mail.local", "123456", false);
            GenerateOneUser("GastroFreak", "gastrofreak@mail.local", "123456", false);
            GenerateOneUser("FoodMaster27", "food.master27@mail.local", "123456", false);
            GenerateOneUser("MeatEater1985", "MeatEater1985@mail.local", "123456", false);
            GenerateOneUser("Goofy222", "goofy222@mail.local", "123456", false);
        }

        private static void GenerateOneUser(string username, string email, string password, bool isAdmin)
        {
            UserRepository userRepository = new UserRepository(connString);

            testUsernames.Add(username);

            // no need for creating the user if it already exists
            if (userRepository.GetAll("username", username).Any())
            {
                return;
            }

            User user = new User()
            {
                Username = username,
                Email = email,
                Password = password,
                IsAdmin = false
            };

            userRepository.Create(user);
        }
    }
}
