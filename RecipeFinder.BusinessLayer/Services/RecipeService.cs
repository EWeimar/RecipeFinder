using RecipeFinder.BusinessLayer.Interfaces;
using RecipeFinder.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.BusinessLayer.Services
{
    public class RecipeService : IRecipeService
    {
        private Recipe _recipe = new Recipe();

        public void Create (Recipe recipe)
        {
            _recipe.Create(recipe);
        }


        public Recipe GetRecipe(string title)
        {
            throw new NotImplementedException();
        }

        public Recipe GetRecipe(int id)
        {
            throw new NotImplementedException();
        }
    }
}
