using RecipeFinder.DataLayer.Models;
using RecipeFinder.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace RecipeFinder.WebClient.Models
{
    public class RecipeModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string RowVer { get; set; }
        public List<IngredientLineModel> IngredientLines { get; set; }
        public string Instruction { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ImageModel> Images { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ImageModel GetFirstImage()
        {
            ImageModel res = new ImageModel() {FileName = "http://placehold.it/750x500" };

            if (Images.Any())
            {
                res = Images.FirstOrDefault();
            }

            return res;
        }

    }
}