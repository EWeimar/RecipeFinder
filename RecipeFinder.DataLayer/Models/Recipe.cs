using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.DataLayer.Models
{
    public class Recipe
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Instruction { get; set; }
        public DateTime CreatedAt { get; set; }

        public Recipe()
        {
            IngredientLines = new List<IngredientLine>();
        }

        public List<IngredientLine> IngredientLines { get; set; }
        public Image Image { get; set; }
        public RecipeRating RecipeRating { get; set; }
        public RecipeComment RecipeComment { get; set; }
        public User UserId { get; set; }
    }
}
