using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeFinder.DataLayer.Models;
using RecipeFinder.DataLayer.Repositories;
using System;
using System.Threading.Tasks;

namespace RecipeFinder.Tests.Repositories
{
    [TestClass]
    public class IngredientLineRepositoryTests
    {
        private const string connString = "Data Source=.\\SQLExpress;Initial Catalog=RecipeFinderDB_TEST;Integrated Security=True;";
        private static IngredientLineRepository ingredientLineRepository;

        [ClassInitialize]
        public static void TestSuiteInitialize(TestContext testContext)
        {
            //Is run once for all tests in file
            ingredientLineRepository = new IngredientLineRepository(connString);
            DBHelper.CleanDatabase(connString);
            DBHelper.CreateTestData(connString);
        }

        [ClassCleanup]
        public static void TestSuiteCleanup()
        {
            //Is run once for all tests in file
            ingredientLineRepository = null;
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
        public async Task InsertNewIngredientLine()
        {
            //Arrange
            IngredientLine ingredientLine = new IngredientLine();
            ingredientLine.Id = 0;
            ingredientLine.RecipeId = 1;
            ingredientLine.IngredientId = 1;
            ingredientLine.Amount = 2;
            ingredientLine.MeasureUnit = MeasureUnit.Stk;

            //Act
            var addResult = await ingredientLineRepository.AddAsync(ingredientLine);

            //Assert
            Assert.IsNotNull(addResult);
            Assert.AreEqual(ingredientLine.RecipeId, addResult.RecipeId);
            Assert.AreEqual(ingredientLine.IngredientId, addResult.IngredientId);
            Assert.AreEqual(ingredientLine.Amount, addResult.Amount);
            Assert.AreEqual(ingredientLine.MeasureUnit, addResult.MeasureUnit);

        }

        [TestMethod]
        [Priority(1)]
        public async Task GetIngredientLine()
        {
            //Arrange
            IngredientLine ingredientLine = new IngredientLine();
            ingredientLine.Id = 0;
            ingredientLine.RecipeId = 1;
            ingredientLine.IngredientId = 1;
            ingredientLine.Amount = 2;
            ingredientLine.MeasureUnit = MeasureUnit.Stk;

            //Act
            var addResult = await ingredientLineRepository.AddAsync(ingredientLine);
            var getResult = await ingredientLineRepository.GetByIdAsync(addResult.Id);

            //Assert
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(getResult);
            Assert.AreEqual(addResult.Id, getResult.Id);
            Assert.AreEqual(addResult.Amount, getResult.Amount);
            Assert.AreEqual(addResult.MeasureUnit, getResult.MeasureUnit);

        }

        [TestMethod]
        [Priority(2)]
        public async Task UpdateIngredientLine()
        {
            //Arrange
            IngredientLine ingredientLine = new IngredientLine();
            ingredientLine.Id = 0;
            ingredientLine.RecipeId = 1;
            ingredientLine.IngredientId = 1;
            ingredientLine.Amount = 2;
            ingredientLine.MeasureUnit = MeasureUnit.Stk;

            //Act
            var addResult = await ingredientLineRepository.AddAsync(ingredientLine);
            addResult.Amount = 5;
            addResult.MeasureUnit = MeasureUnit.Kg;
            var updateResult = await ingredientLineRepository.UpdateAsync(addResult);

            //Assert
            Assert.AreEqual(1, updateResult);
            var getResult = await ingredientLineRepository.GetByIdAsync(addResult.Id);
            Assert.AreEqual(5, getResult.Amount);
            Assert.AreEqual(MeasureUnit.Kg, getResult.MeasureUnit);

        }

        [TestMethod]
        [Priority(3)]
        public async Task DeleteIngredientLine()
        {
            //Arrange
            IngredientLine ingredientLine = new IngredientLine();
            ingredientLine.Id = 0;
            ingredientLine.RecipeId = 1;
            ingredientLine.IngredientId = 1;
            ingredientLine.Amount = 2;
            ingredientLine.MeasureUnit = MeasureUnit.Stk;

            //Act
            var addResult = await ingredientLineRepository.AddAsync(ingredientLine);

            //Assert
            Assert.IsNotNull(addResult);
            var deleteResult = await ingredientLineRepository.DeleteAsync(addResult.Id);
            Assert.AreEqual(1, deleteResult);
            
            try
            {
                var getResult = await ingredientLineRepository.GetByIdAsync(addResult.Id);
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
        public async Task GetNonExistentIngredientLine()
        {
            //Arrange
            //Entities with id = 0, will never exist

            //Act
            var getResult = await ingredientLineRepository.GetByIdAsync(0);

            //Assert
            //Assert is done by annotation

        }
    }
}
