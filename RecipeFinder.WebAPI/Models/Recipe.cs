using RecipeFinder.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeFinder.WebAPI.Models
{
    public class Recipe
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public MeasureUnit Unit { get; set; }

    }
}