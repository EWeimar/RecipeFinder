using RecipeFinder.DataLayer.Models;
using RecipeFinder.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeFinder.WebClient.Models
{
    public class RecipeModel
    {
        public int RecipeId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public List<IngredientLineModel> IngredientLines { get; set; }
        public string Instructions { get; set; }
        public List<ImageModel> Images { get; set; }
    }
}