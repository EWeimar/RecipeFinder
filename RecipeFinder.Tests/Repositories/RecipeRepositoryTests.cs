using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeFinder.DataLayer.Models;
using RecipeFinder.DataLayer.Repositories;
using System;
using System.Threading.Tasks;

namespace RecipeFinder.Tests.Repositories
{
    [TestClass]
    public class RecipeRepositoryTests
    {
        private const string connString = "Data Source=.\\SQLExpress;Initial Catalog=RecipeFinderDB_TEST;Integrated Security=True;";
        private static RecipeRepository recipeRepository;

        [ClassInitialize]
        public static void TestSuiteInitialize(TestContext testContext)
        {
            //Is run once for all tests in file
            recipeRepository = new RecipeRepository(connString);
            DBHelper.CleanDatabase(connString);
            DBHelper.CreateTestData(connString);
        }

        [ClassCleanup]
        public static void TestSuiteCleanup()
        {
            //Is run once for all tests in file
            recipeRepository = null;
            DBHelper.CleanDatabase(connString);
        }

        [TestInitialize]
        public async Task Setup()
        {
            //Is run before each tests in file

        }

        [TestCleanup]
        public async Task TearDown()
        {
            //Is run after each tests in file

        }

        [TestMethod]
        [Priority(0)]
        public async Task InsertNewRecipe()
        {
            //Arrange
            Recipe recipe = new Recipe();
            recipe.Id = 0;
            recipe.UserId = 1;
            recipe.Title = "recipe1";
            recipe.Slug = "00-recipe1";
            recipe.Instruction = "instruction1";
            recipe.CreatedAt = DateTime.Now;

            //Act
            var addResult = await recipeRepository.AddAsync(recipe);

            //Assert
            Assert.IsNotNull(addResult);
            Assert.AreEqual(recipe.Title, addResult.Title);
            Assert.AreEqual(recipe.UserId, addResult.UserId);
            Assert.AreEqual(recipe.Instruction, addResult.Instruction);

        }

        [TestMethod]
        [Priority(1)]
        public async Task GetRecipe()
        {
            //Arrange
            Recipe recipe = new Recipe();
            recipe.Id = 0;
            recipe.UserId = 1;
            recipe.Title = "recipe2";
            recipe.Slug = "00-recipe2";
            recipe.Instruction = "instruction2";
            recipe.CreatedAt = DateTime.Now;

            //Act
            var addResult = await recipeRepository.AddAsync(recipe);
            var getResult = await recipeRepository.GetByIdAsync(addResult.Id);

            //Assert
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(getResult);
            Assert.AreEqual(addResult.Id, getResult.Id);
            Assert.AreEqual(addResult.Title, getResult.Title);
            Assert.AreEqual(addResult.Instruction, getResult.Instruction);

        }

        [TestMethod]
        [Priority(2)]
        public async Task UpdateRecipe()
        {
            //Arrange
            Recipe recipe = new Recipe();
            recipe.Id = 0;
            recipe.UserId = 1;
            recipe.Title = "recipe3";
            recipe.Slug = "00-recipe3";
            recipe.Instruction = "instruction3";
            recipe.CreatedAt = DateTime.Now;

            //Act
            var addResult = await recipeRepository.AddAsync(recipe);
            addResult.Title = "recipe3Update";
            addResult.Slug = "00-recipe3Update";
            addResult.Instruction = "instruction3Update";
            var updateResult = await recipeRepository.UpdateAsync(addResult);

            //Assert
            Assert.AreEqual(1, updateResult);
            var getResult = await recipeRepository.GetByIdAsync(addResult.Id);
            Assert.AreEqual("recipe3Update", getResult.Title);
            Assert.AreEqual("00-recipe3Update", getResult.Slug);
            Assert.AreEqual("instruction3Update", getResult.Instruction);

        }

        [TestMethod]
        [Priority(3)]
        public async Task DeleteRecipe()
        {
            //Arrange
            Recipe recipe = new Recipe();
            recipe.Id = 0;
            recipe.UserId = 1;
            recipe.Title = "recipe4";
            recipe.Slug = "00-recipe4";
            recipe.Instruction = "instruction4";
            recipe.CreatedAt = DateTime.Now;

            //Act
            var addResult = await recipeRepository.AddAsync(recipe);

            //Assert
            Assert.IsNotNull(addResult);
            var deleteResult = await recipeRepository.DeleteAsync(addResult.Id);
            Assert.AreEqual(1, deleteResult);

            try
            {
                var getResult = await recipeRepository.GetByIdAsync(addResult.Id);
            }
            catch (InvalidOperationException)
            {
                //Success.
            }
            catch (Exception)
            {
                //Failure.
                //This assert statement ensures failure.
                Assert.AreEqual(1, 2);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        [Priority(4)]
        public async Task GetNonExistentRecipe()
        {
            //Arrange
            //Entities with id = 0, will never exist

            //Act
            var getResult = await recipeRepository.GetByIdAsync(0);

            //Assert
            //Assert is done by annotation

        }
    }
}
