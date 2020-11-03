using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.DataLayer.Models
{
    public class IngredientLine
    {
        public Enum MeasureUnit { get; set; }
        public int Amount { get; set; }
    }
}
