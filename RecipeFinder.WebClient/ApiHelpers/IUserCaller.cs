using RecipeFinder.WebClient.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.WebClient.ApiHelpers
{
    interface IUserCaller
    {
        RFApiResult CreateUser(User user);
    }
}
