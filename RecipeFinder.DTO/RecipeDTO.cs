using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.DTO
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Instruction { get; set; }
        public DateTime? CreatedAt { get; set; } //Optional
        public List<IngredientLineDTO> IngredientLines { get; set; }
        public List<ImageDTO> Images { get; set; } //Optional
    }
}
