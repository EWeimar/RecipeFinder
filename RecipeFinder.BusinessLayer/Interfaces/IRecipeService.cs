using RecipeFinder.DataLayer.Models;
using RecipeFinder.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeFinder.BusinessLayer.Interfaces
{
    public interface IRecipeService
    {
        Task <Recipe> AddAsync (RecipeDTO recipe);
        Task <Recipe> GetByIdAsync(int id);
        Task <IEnumerable<RecipeDTO>> GetAllAsync();
        Task<int> UpdateAsync(RecipeDTO recipe);
        Task<int> DeleteAsync(RecipeDTO recipe);
        Task<List<MeasureUnit>> GetAllMeasureUnits();
    }
}
