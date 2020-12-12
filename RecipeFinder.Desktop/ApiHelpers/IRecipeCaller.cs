using RecipeFinder.DTO;
using RecipeFinder.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeFinder.Desktop.ApiHelpers
{
    public interface IRecipeCaller
    {
        RFApiResult CreateRecipe(RecipeDTO recipeDTO);

        RFApiResult UpdateRecipe(object recipeDTO);

        RecipeList GetAll();
        RecipeModel FindByCondition(string propName, object value);
    }
}