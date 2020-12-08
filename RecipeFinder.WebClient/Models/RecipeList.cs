using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace RecipeFinder.WebClient.Models
{
    public class RecipeList
    {
        public List<RecipeModel> recipes;
        public HttpStatusCode StatusCode;        
    }
}