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
        public MeasureUnit MeasureUnit { get; set; }
        public int Amount { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
