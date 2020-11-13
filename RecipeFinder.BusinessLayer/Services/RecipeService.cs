using RecipeFinder.BusinessLayer.Interfaces;
using RecipeFinder.DataLayer.Models;
using RecipeFinder.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.BusinessLayer.Services
{
    public class RecipeService : IRecipeService
    {
        IRepository<Recipe> repo; 
        public RecipeService()
        {
            repo = new RecipeRepository("Data Source=.\\SQLExpress;Initial Catalog=RecipeFinderDB;Integrated Security=True;");
        }

        public void Create (Recipe recipe)
        {
            repo.Create(recipe);
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
