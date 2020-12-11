using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.DTO
{
    public class RecipeDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User is required")]
        public UserDTO User { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string Slug { get; set; }

        [Required(ErrorMessage = "Instructions are required")]
        public string Instruction { get; set; }
        public DateTime? CreatedAt { get; set; } //Optional

        [Required(ErrorMessage = "Ingredients are required")]
        public List<IngredientLineDTO> IngredientLines { get; set; }
        public List<ImageDTO> Images { get; set; } //Optional
        public byte[] RowVer { get; set; }
    }
}
