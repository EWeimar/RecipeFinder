using RecipeFinder.DataLayer.Models;
using RecipeFinder.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
