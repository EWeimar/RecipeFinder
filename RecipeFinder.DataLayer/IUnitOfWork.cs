using RecipeFinder.DataLayer.Models;
using RecipeFinder.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.DataLayer
{
    public interface IUnitOfWork
    {
        IRepository<Image> Images { get; }
        IRepository<Ingredient> Ingredients { get; }
        IRepository<IngredientLine> IngredientLines { get; }
        IRepository<Recipe> Recipes { get; }
        IRepository<RecipeReview> RecipeReviews { get; }
        IRepository<UserFavorite> UserFavorites { get; }
        IUserRepository<User> Users { get; }
    }
}
