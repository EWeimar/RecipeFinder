using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.DataLayer.Models
{
    public class RecipeComment
    {
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
