using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.DataLayer.Models
{
    public class RecipeImage
    {
        public Image ImageId { get; set; }
        public Recipe RecipeId { get; set; }
    }
}
