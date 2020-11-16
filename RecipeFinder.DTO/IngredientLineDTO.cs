using RecipeFinder.DataLayer.Models;

namespace RecipeFinder.DTO
{
    public class IngredientLineDTO
    {
        public int Id { get; set; }
        public IngredientDTO Ingredient { get; set; }
        public decimal Amount { get; set; }
        public MeasureUnit MeasureUnit { get; set; }
        
    }
}