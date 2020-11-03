using RecipeFinder.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecipeFinder.WebAPI.Controllers
{
    public class RecipeController : ApiController
    {
        public List<Recipe> GetRecipes()
        {
            return new List<Recipe>()
            {
                new Recipe()
                {
                    Name = "Pølser",
                    Description = "Dejlige store pølser",
                    Rating = 5,
                    Unit = DataLayer.Models.MeasureUnit.L
                    
                }
            };
        }
    }
}
