using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.DataLayer.Models
{
    public class RecipeReview
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Reviewer { get; set; }
        public int? Rating { get; set; } //Optional
        public string Comment { get; set; }  //Optional
        public DateTime CreatedAt { get; set; }

    }
}
