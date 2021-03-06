﻿using System;
using RecipeFinder.DataLayer.Models;
using RestSharp;
using RecipeFinder.DataLayer.Repositories;
using RecipeFinder.DevTools.Commands.MealDB;
using RecipeFinder.DTO;
using System.Collections.Generic;
using RecipeFinder.BusinessLayer.Services;
using System.Linq;
using System.Threading;
using RecipeFinder.BusinessLayer.Lib;
using RecipeFinder.DevTools.Util;

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

        private static async void GenerateRandomDummyRecipe()
        {
            var client = new RestClient("https://www.themealdb.com/api/json/v1/1/");
            var request = new RestRequest("random.php", Method.GET);
            var queryResult = client.Execute<MealDBRecipeList>(request).Data;

            /** Working around another hack :(((( made in RecipeService, due to CreateRecipe feature  **/
            var rnd = new Random();
            var measureUnitsMax = Enum.GetValues(typeof(MeasureUnit)).Length;
            string[] measureUnits = new string[11];
            measureUnits[0] = "-";
            measureUnits[1] = "ml";
            measureUnits[2] = "cl";
            measureUnits[3] = "dl";
            measureUnits[4] = "l";
            measureUnits[5] = "g";
            measureUnits[6] = "kg";
            measureUnits[7] = "tsk";
            measureUnits[8] = "spsk";
            measureUnits[9] = "knsp";
            measureUnits[10] = "stk";

            RecipeRepository recipeRepository = new RecipeRepository(connString);
            IngredientLineRepository ingredientLineRepository = new IngredientLineRepository(connString);
            UserRepository userRepository = new UserRepository(connString);

            foreach (MealDBRecipe mealDbRecipe in queryResult.meals)
            {
                IEnumerable<Recipe> existingRecipeWithSameName = recipeRepository.FindByCondition(nameof(Recipe.Title), mealDbRecipe.strMeal).Result.ToList();

                // if it is already existing, skip
                if (existingRecipeWithSameName.Any())
                {
                    continue;
                }

                RecipeService rs = new RecipeService();

                string randomUserName = testUsernames[new Random().Next(0, testUsernames.Count - 1)];
                User randomUser = userRepository.FindByCondition("username", randomUserName).Result.Single();

                RecipeDTO obj = new RecipeDTO()
                {
                    Id = 0,
                    User = new UserDTO() { Id = randomUser.Id },
                    Title = mealDbRecipe.strMeal,
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

                                var _saffsaaf = measureUnits.GetValue(rnd.Next(Enum.GetNames(typeof(MeasureUnit)).Length));

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
                                        Amount = rnd.Next(1, 11),
                                        MeasureUnitText = measureUnits[rnd.Next(measureUnitsMax)]
                                    });
                                }

                                //measureUnitsMax

                            }
                            catch (Exception e)
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

                await rs.AddAsync(obj);
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
            if (userRepository.FindByCondition("username", username).Result.Any())
            {
                return;
            }

            User user = new User()
            {
                Username = username,
                Email = email,
                Password = SecurePasswordHasher.Hash(password),
                IsAdmin = false
            };

            var addResult = userRepository.AddAsync(user);

            if (addResult.Result == null)
            {
                Console.WriteLine("A user could not be created");
            }

        }
    }
}
