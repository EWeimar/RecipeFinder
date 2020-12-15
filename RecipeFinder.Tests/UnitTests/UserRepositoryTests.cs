using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeFinder.DataLayer.Models;
using RecipeFinder.DataLayer.Repositories;
using System;
using System.Threading.Tasks;

namespace RecipeFinder.Tests.UnitTests
{
    [TestClass]
    public class UserRepositoryTests
    {
        private const string connString = "Data Source=.\\SQLExpress;Initial Catalog=RecipeFinderDB;Integrated Security=True;";
        private static UserRepository userRepository;

        [ClassInitialize]
        public static void TestSuiteInitialize(TestContext testContext)
        {
            //Is run once for all tests in file
            userRepository = new UserRepository(connString);

        }

        [ClassCleanup]
        public static void TestSuiteCleanup()
        {
            //Is run once for all tests in file
            userRepository = null;
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
        public async Task CreateNewUser()
        {
            //Arrange
            User user = new User();
            user.Id = 0;
            user.Username = "user1";
            user.Email = "user1Email";
            user.Password = "user1Password";
            user.IsAdmin = false;
            user.CreatedAt = DateTime.Now;

            //Act
            var addResult = await userRepository.AddAsync(user);

            //Assert
            Assert.IsNotNull(addResult);
            Assert.AreEqual(user.Username, addResult.Username);
            Assert.AreEqual(user.Email, addResult.Email);
            Assert.AreEqual(user.Password, addResult.Password);

        }

        [TestMethod]
        [Priority(1)]
        public async Task GetRecipe()
        {
            //Arrange
            User user = new User();
            user.Id = 0;
            user.Username = "user2";
            user.Email = "user2Email";
            user.Password = "user2Password";
            user.IsAdmin = false;
            user.CreatedAt = DateTime.Now;

            //Act
            var addResult = await userRepository.AddAsync(user);
            var getResult = await userRepository.GetByIdAsync(addResult.Id);

            //Assert
            Assert.IsNotNull(addResult);
            Assert.IsNotNull(getResult);
            Assert.AreEqual(addResult.Id, getResult.Id);
            Assert.AreEqual(addResult.Username, getResult.Username);
            Assert.AreEqual(addResult.Email, getResult.Email);

        }

        [TestMethod]
        [Priority(2)]
        public async Task UpdateRecipe()
        {
            //Arrange
            User user = new User();
            user.Id = 0;
            user.Username = "user3";
            user.Email = "user3Email";
            user.Password = "user3Password";
            user.IsAdmin = false;
            user.CreatedAt = DateTime.Now;

            //Act
            var addResult = await userRepository.AddAsync(user);
            addResult.Username = "user3Update";
            addResult.Email = "user3EmailUpdate";
            addResult.Password = "user3PasswordUpdate";
            var updateResult = await userRepository.UpdateAsync(addResult);

            //Assert
            Assert.AreEqual(1, updateResult);
            var getResult = await userRepository.GetByIdAsync(addResult.Id);
            Assert.AreEqual("user3Update", getResult.Username);
            Assert.AreEqual("user3EmailUpdate", getResult.Email);
            Assert.AreEqual("user3PasswordUpdate", getResult.Password);

        }

        [TestMethod]
        [Priority(3)]
        public async Task DeleteRecipe()
        {
            //Arrange
            User user = new User();
            user.Id = 0;
            user.Username = "user4";
            user.Email = "user4Email";
            user.Password = "user4Password";
            user.IsAdmin = false;
            user.CreatedAt = DateTime.Now;

            //Act
            var addResult = await userRepository.AddAsync(user);

            //Assert
            Assert.IsNotNull(addResult);
            var deleteResult = await userRepository.DeleteAsync(addResult.Id);
            Assert.IsNotNull(deleteResult);
            Assert.AreEqual(1, deleteResult);


        }
    }
}
