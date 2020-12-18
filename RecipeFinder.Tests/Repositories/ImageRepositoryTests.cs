using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeFinder.DataLayer.Models;
using RecipeFinder.DataLayer.Repositories;
using System;
using System.Threading.Tasks;

namespace RecipeFinder.Tests.Repositories
{
    [TestClass]
    public class ImageRepositoryTests
    {
        private const string connString = "Data Source=.\\SQLExpress;Initial Catalog=RecipeFinderDB_TEST;Integrated Security=True;";
        private static ImageRepository imageRepository;

        [ClassInitialize]
        public static void TestSuiteInitialize(TestContext testContext)
        {
            //Is run once for all tests in file
            imageRepository = new ImageRepository(connString);
            DBHelper.CleanDatabase(connString);
            DBHelper.CreateTestData(connString);
        }

        [ClassCleanup]
        public static void TestSuiteCleanup()
        {
            //Is run once for all tests in file
            imageRepository = null;
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
        public async Task InsertNewImage()
        {
            //Arrange
            Image image = new Image();
            image.Id = 0;
            image.RecipeId = 1;
            image.FileName = "image1";

            //Act
            var addResult = await imageRepository.AddAsync(image);

            //Assert
            Assert.IsNotNull(addResult);
            Assert.AreEqual(image.FileName, addResult.FileName);
            Assert.AreEqual(image.RecipeId, addResult.RecipeId);

        }

        [TestMethod]
        [Priority(1)]
        public async Task GetImage()
        {
            //Arrange
            Image image = new Image();
            image.Id = 0;
            image.RecipeId = 1;
            image.FileName = "image2";

            //Act
            var addResult = await imageRepository.AddAsync(image);
            var getResult = await imageRepository.GetByIdAsync(addResult.Id);

            //Assert
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(getResult);
            Assert.AreEqual(addResult.Id, getResult.Id);
            Assert.AreEqual(addResult.FileName, getResult.FileName);

        }

        [TestMethod]
        [Priority(2)]
        public async Task UpdateImage()
        {
            //Arrange
            Image image = new Image();
            image.Id = 0;
            image.RecipeId = 1;
            image.FileName = "image3";

            //Act
            var addResult = await imageRepository.AddAsync(image);
            addResult.FileName = "image3Update";
            var updateResult = await imageRepository.UpdateAsync(addResult);

            //Assert
            Assert.AreEqual(1, updateResult);
            var getResult = await imageRepository.GetByIdAsync(addResult.Id);
            Assert.AreEqual("image3Update", addResult.FileName);


        }

        [TestMethod]
        [Priority(3)]
        public async Task DeleteImage()
        {
            //Arrange
            Image image = new Image();
            image.Id = 0;
            image.RecipeId = 1;
            image.FileName = "image3";

            //Act
            var addResult = await imageRepository.AddAsync(image);

            //Assert
            Assert.IsNotNull(addResult);
            var deleteResult = await imageRepository.DeleteAsync(addResult.Id);
            Assert.AreEqual(1, deleteResult);

            try
            {
                var getResult = await imageRepository.GetByIdAsync(addResult.Id);
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
        public async Task GetNonExistentImage()
        {
            //Arrange
            //Entities with id = 0, will never exist

            //Act
            var getResult = await imageRepository.GetByIdAsync(0);

            //Assert
            //Assert is done by annotation

        }

    }
}
