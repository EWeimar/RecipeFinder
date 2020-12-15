using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeFinder.DataLayer.Models;
using RecipeFinder.DataLayer.Repositories;
using System;
using System.Threading.Tasks;

namespace RecipeFinder.Tests.UnitTests
{
    [TestClass]
    public class IngredientRepositoryTests
    {
        private const string connString = "Data Source=.\\SQLExpress;Initial Catalog=RecipeFinderDB;Integrated Security=True;";
        private static IngredientRepository ingredientRepository;

        [ClassInitialize]
        public static void TestSuiteInitialize(TestContext testContext)
        {
            //Is run once for all tests in file
            ingredientRepository = new IngredientRepository(connString);

        }

        [ClassCleanup]
        public static void TestSuiteCleanup()
        {
            //Is run once for all tests in file
            ingredientRepository = null;
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
        public async Task InsertNewIngredient()
        {
            //Arrange
            Ingredient ingredient = new Ingredient();
            ingredient.Id = 0;
            ingredient.Name = "ingredient1";

            //Act
            var addResult = await ingredientRepository.AddAsync(ingredient);

            //Assert
            Assert.IsNotNull(addResult);
            Assert.AreEqual(ingredient.Name, addResult.Name);
        }

        [TestMethod]
        [Priority(1)]
        public async Task InsertExistingIngredient()
        {
            //Arrange
            Ingredient ingredient = new Ingredient();
            ingredient.Id = 0;
            ingredient.Name = "ingredient2";

            //Act
            var addResult = await ingredientRepository.AddAsync(ingredient);
            var existingResult = await ingredientRepository.AddAsync(ingredient);

            //Assert
            Assert.IsNotNull(addResult);
            Assert.AreEqual(ingredient.Name, addResult.Name);
            Assert.IsNotNull(existingResult);
            Assert.AreEqual(ingredient.Name, existingResult.Name);
            Assert.AreNotEqual(addResult.Id, existingResult.Id);

        }

        [TestMethod]
        [Priority(2)]
        public async Task GetIngredient()
        {
            //Arrange
            Ingredient ingredient = new Ingredient();
            ingredient.Id = 0;
            ingredient.Name = "ingredient3";

            //Act
            var addResult = await ingredientRepository.AddAsync(ingredient);
            var getResult = await ingredientRepository.GetByIdAsync(addResult.Id);

            //Assert
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(getResult);
            Assert.AreEqual(addResult.Id, getResult.Id);
            Assert.AreEqual(addResult.Name, getResult.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        [Priority(3)]
        public async Task GetNonExistentIngredient()
        {
            //Arrange
            //Entities with id = 0, will never exist

            //Act
            var getResult = await ingredientRepository.GetByIdAsync(0);

            //Assert
            //Assert is done by annotation

        }

        [TestMethod]
        [Priority(4)]
        public async Task UpdateIngredient()
        {
            //Arrange
            Ingredient ingredient = new Ingredient();
            ingredient.Id = 0;
            ingredient.Name = "ingredient4";

            //Act
            var addResult = await ingredientRepository.AddAsync(ingredient);
            addResult.Name = "i4update";
            var updateResult = await ingredientRepository.UpdateAsync(addResult);

            //Assert
            Assert.AreEqual(1, updateResult);
            var getResult = await ingredientRepository.GetByIdAsync(addResult.Id);
            Assert.AreEqual("i4update", getResult.Name);

        }

        [TestMethod]
        [Priority(5)]
        public async Task DeleteIngredient()
        {
            //Arrange
            Ingredient ingredient = new Ingredient();
            ingredient.Id = 0;
            ingredient.Name = "ingredient5";

            //Act
            var addResult = await ingredientRepository.AddAsync(ingredient);
            

            //Assert
            Assert.IsNotNull(addResult);
            var deleteResult = await ingredientRepository.DeleteAsync(addResult.Id);
            Assert.IsNotNull(deleteResult);
            Assert.AreEqual(1, deleteResult);
        }
    }
}
