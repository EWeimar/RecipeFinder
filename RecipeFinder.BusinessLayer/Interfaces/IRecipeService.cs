using RecipeFinder.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.BusinessLayer.Interfaces
{
    public interface IRecipeService
    {
        Recipe GetRecipe(string title);
        Recipe GetRecipe(int id);


        
    }
}
