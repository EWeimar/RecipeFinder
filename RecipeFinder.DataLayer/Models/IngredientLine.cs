using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.DataLayer.Models
{
    public class IngredientLine
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public decimal Amount { get; set; }
        public MeasureUnit MeasureUnit { get; set; }

    }
}
