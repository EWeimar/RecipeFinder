using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace RecipeFinder.WebClient.Models
{
    public class RFApiResult
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool Success { get; set; }
    }
}