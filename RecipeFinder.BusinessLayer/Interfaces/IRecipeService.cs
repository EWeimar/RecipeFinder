using RecipeFinder.DataLayer.Models;
using RecipeFinder.DTO;
using System.Collections.Generic;

namespace RecipeFinder.BusinessLayer.Interfaces
{
    public interface IRecipeService
    {
        void Create(RecipeDTO recipe);
        RecipeDTO Get(int id);
        List<RecipeDTO> GetAll();
        void Update(RecipeDTO recipe);
        void Delete(RecipeDTO recipe);
    }
}
