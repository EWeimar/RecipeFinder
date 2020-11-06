using RecipeFinder.DataLayer.Models;
using System.Collections.Generic;

namespace RecipeFinder.BusinessLayer.Commands.ApiHelpers
{
    public interface IDummyRecipesCaller
    {
        List<Recipe> GetRecipes();
        Recipe GetRecipe(int id);
        void Create(Recipe Recipe);
        void Update(int id, Recipe Recipe);
        void Delete(int id);
    }
}