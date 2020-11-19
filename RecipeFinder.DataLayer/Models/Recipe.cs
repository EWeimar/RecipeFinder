using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.DataLayer.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Instruction { get; set; }
        public List<IngredientLine> IngredientLines { get; set; }
        public DateTime CreatedAt { get; set; }     
        
    }
}
