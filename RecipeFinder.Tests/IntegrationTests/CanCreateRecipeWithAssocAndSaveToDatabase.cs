using System;
using System.Reflection;
using System.Threading.Tasks;
using DbUp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeFinder.DataLayer.Models;
using RecipeFinder.DataLayer.Repositories;

namespace RecipeFinder.Tests.IntegrationTests
{
    [TestClass]
    public class CanCreateRecipeWithAssocAndSaveToDatabase
    {
        private readonly string connStr = "Data Source=.\\SQLExpress;Initial Catalog=RecipeFinderDB;Integrated Security=True;";
        RecipeRepository recipeRepository;
        IngredientRepository ingredientRepository;
        IngredientLineRepository ingredientLineRepository;
        UserRepository userRepository;

        int recipeId;
        int ingredientId;

        [TestInitialize]
        public async Task TestInitializeAsync()
        {
            recipeRepository = new RecipeRepository(connStr);
            ingredientRepository = new IngredientRepository(connStr);
            ingredientLineRepository = new IngredientLineRepository(connStr);
            userRepository = new UserRepository(connStr);

            //User existingTestUser = await userRepository.GetByIdAsync(1);

            //Console.WriteLine("Test: "+existingTestUser.Id);

            //if (existingTestUser == null)
            //{
            //    await userRepository.AddAsync(new User() { Username = "TestUser", Email = "tester@testing.local", CreatedAt = DateTime.Now, Password = "123456", IsAdmin = false });
            //}
        }

        // arrange
        // act
        // assert

        //[TestMethod]
        //[Priority(0)]
        //public async Task TestMethod1()
        //{
        //    User res = await userRepository.AddAsync(new User() { Username = "Lalalla"+ System.Guid.NewGuid(), Email = "adssd@"+ System.Guid.NewGuid() + ".com", CreatedAt = DateTime.Now, Password = "123", IsAdmin = false });

        //    Assert.IsTrue(res.Id == 1);
        //}

        //CanCreateRecipe
        [TestMethod]
        [Priority(0)]
        public async Task TestMethod2()
        {
            // arrange
            string recipeTitle = "A recipe created from test";
            string recipeSlug = "a-recipe-created-from-test" + System.Guid.NewGuid();
            string recipeInstruction = "The test recipe instruction text string";

            Recipe recipe = new Recipe();
            recipe.Title = recipeTitle;
            recipe.Slug = recipeSlug;
            recipe.Instruction = recipeInstruction;
            recipe.UserId = 1;
            recipe.CreatedAt = DateTime.Now;

            // act
            Recipe recipeSaved = await recipeRepository.AddAsync(recipe);

            // assert

            Assert.IsNotNull(recipeSaved);

            // checking that the values we inserted is equal to the values of the entity returned from the add method
            Assert.AreEqual(recipeTitle, recipeSaved.Title);
            Assert.AreEqual(recipeSlug, recipeSaved.Slug);
            Assert.AreEqual(recipeInstruction, recipeSaved.Instruction);

            Assert.IsTrue(recipeSaved.Id > 0);

            this.recipeId = recipeSaved.Id;
        }

        //_CanCreateNewIngredient
        [TestMethod]
        [Priority(1)]
        public async Task TestMethod3()
        {
            // arrange
            Ingredient ingredient = new Ingredient() { Name = "Our Test Ingredient" };

            // act
            Ingredient ingredientSaved = await ingredientRepository.AddAsync(ingredient);

            // assert
            Assert.IsNotNull(ingredientSaved);
            Assert.AreEqual(ingredient.Name, ingredientSaved.Name);

            this.ingredientId = ingredientSaved.Id;
        }

        ////Step3_CanAddIngredientLineOnAlreadyCreatedRecipe
        [TestMethod]
        [Priority(2)]
        public async Task TestMethod4()
        {
            // arrange
            IngredientLine ingredientLine = new IngredientLine()
            {
                RecipeId = 1,
                IngredientId = 1,
                Amount = 5,
                MeasureUnit = MeasureUnit.Spsk
            };

            // act
            IngredientLine ingredientLineSaved = await ingredientLineRepository.AddAsync(ingredientLine);

            // assert
            Assert.IsNotNull(ingredientLineSaved);
            Assert.AreEqual(ingredientLine.IngredientId, ingredientLineSaved.IngredientId);
            Assert.AreEqual(ingredientLine.RecipeId, ingredientLineSaved.RecipeId);
            Assert.AreEqual(ingredientLine.Amount, ingredientLineSaved.Amount);
            Assert.AreEqual(ingredientLine.MeasureUnit, ingredientLineSaved.MeasureUnit);
        }
    }
}
