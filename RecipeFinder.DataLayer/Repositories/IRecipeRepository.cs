using System.Threading.Tasks;

namespace RecipeFinder.DataLayer.Repositories
{
    public interface IRecipeRepository<Recipe> : IRepository<Recipe>
    {
        //Task<Recipe> GetBySlugAsync(string slug);
    }
}