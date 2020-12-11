using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeFinder.WebClient.Models
{
    public class ImageModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }

        public string GetUrl()
        {
            string imageUrl = "http://placehold.it/750x500";

            if (FileName.StartsWith("http"))
            {
                imageUrl = FileName;
            }
            else
            {
                imageUrl = String.Format("/Content/Images/{0}", FileName);
            }

            return imageUrl;
        }
    }
}