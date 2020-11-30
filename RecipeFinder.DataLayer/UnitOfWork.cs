using RecipeFinder.DataLayer.Models;
using RecipeFinder.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private const string CONNECTION_STRING_NAME = "ConnectionString";

        //Readonly to avoid external tampering (E.g. Setting repository = null)
        private readonly IRepository<Image> imageRepository;
        private readonly IRepository<Ingredient> ingredientRepository;
        private readonly IRepository<IngredientLine> ingredientLineRepository;
        private readonly IRepository<Recipe> recipeRepository;
        private readonly IRepository<RecipeReview> recipeReviewRepository;
        private readonly IRepository<UserFavorite> userFavoriteRepository;
        private readonly IUserRepository<User> userRepository;

        //Initialize repositories
        //1 default constructor for easy initialization (Using the default connection string)
        //1 overloaded constructor for specifying the connection string
        public UnitOfWork(string connString)
        {
            imageRepository = new ImageRepository(connString);
            ingredientRepository = new IngredientRepository(connString);
            ingredientLineRepository = new IngredientLineRepository(connString);
            recipeRepository = new RecipeRepository(connString);
            recipeReviewRepository = new RecipeReviewRepository(connString);
            userFavoriteRepository = new UserFavoriteRepository(connString);
            userRepository = new UserRepository(connString);
        }

        //Default constructor chaining overloaded constructor (Calls another constructor)
        public UnitOfWork() : this(ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME].ConnectionString)
        {
            
        }

        public IRepository<Image> Images => imageRepository;
        
        public IRepository<Ingredient> Ingredients => ingredientRepository;

        public IRepository<IngredientLine> IngredientLines => ingredientLineRepository;

        public IRepository<Recipe> Recipes => recipeRepository;

        public IRepository<RecipeReview> RecipeReviews => recipeReviewRepository;

        public IRepository<UserFavorite> UserFavorites => userFavoriteRepository;

        public IUserRepository<User> Users => userRepository;
    }
}
