using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.DevTools.Commands.DummyRecipes.ApiHelpers
{
    interface IRecipeCaller
    {
        List<Recipe> GetRecipes();
        Recipe GetRecipe(int id);
        void Create(Recipe Recipe);
        void Update(int id, Recipe Recipe);
        void Delete(int id);
    }
}
