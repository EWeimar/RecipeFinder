using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeFinder.Desktop.Models
{
    public class IngredientLineModel
    {
        public int Id { get; set; }
        public IngredientModel Ingredient { get; set; }
        public decimal Amount { get; set; }
        public string MeasureUnit { get; set; }
    }
}