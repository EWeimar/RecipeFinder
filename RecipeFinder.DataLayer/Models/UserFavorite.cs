using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.DataLayer.Models
{
    public class UserFavorite
    {
        public Recipe RecipeId { get; set; }
        public User UserId { get; set; }
    }
}
