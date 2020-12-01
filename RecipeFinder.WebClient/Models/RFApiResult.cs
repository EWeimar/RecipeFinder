using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace RecipeFinder.WebClient.Models
{
    public class RFApiResult
    {
        public string Message;
        public HttpStatusCode StatusCode;
        public bool Success;
    }
}