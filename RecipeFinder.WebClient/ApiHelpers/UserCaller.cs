using RecipeFinder.WebClient.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace RecipeFinder.WebClient.ApiHelpers
{
    public class UserCaller : IUserCaller
    {
        private RestClient client;
        public UserCaller(string baseUrl)
        {
            client = new RestClient(baseUrl);
        }

        public RFApiResult CreateUser(User user)
        {
            var request = new RestRequest("/user/create", Method.POST);

            request.AddJsonBody(user);

            IRestResponse<RFApiResult> response = client.Execute<RFApiResult>(request);

            response.Data.StatusCode = response.StatusCode;

            if (response.IsSuccessful)
            {
                response.Data.Success = true;
            } else
            {
                response.Data.Success = false;
            }

            return response.Data;
        }
    }
}