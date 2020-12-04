using RecipeFinder.DataLayer.Models;
using RecipeFinder.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeFinder.WebClient.ApiHelpers
{
    public interface IRecipeCaller
    {
        RFApiResult CreateRecipe(Recipe recipe);
    }
}