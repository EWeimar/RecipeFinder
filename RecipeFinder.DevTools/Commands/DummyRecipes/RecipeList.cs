using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeFinder.DataLayer.Models;

namespace RecipeFinder.DevTools.Commands.DummyRecipes
{
    class RecipeList
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
        public List<Recipe> results { get; set; }
    }
}
