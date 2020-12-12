using RecipeFinder.DataLayer.Models;
using RecipeFinder.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace RecipeFinder.Desktop.Models
{
    public class RecipeModel
    {
        public int Id { get; set; }
        public UserModel User { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public List<IngredientLineModel> IngredientLines { get; set; }
        public string Instruction { get; set; }
        public List<ImageModel> Images { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public string rowVer  { get; set; }

    }
}