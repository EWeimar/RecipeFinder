using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.DataLayer.Models
{
    public class RecipeList
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
        public List<Recipe> results { get; set; }
    }
}
