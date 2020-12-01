using RecipeFinder.BusinessLayer.Exceptions;
using RecipeFinder.BusinessLayer.Interfaces;
using RecipeFinder.BusinessLayer.Services;
using RecipeFinder.DataLayer.Models;
using RecipeFinder.DataLayer.Repositories;
using RecipeFinder.DevTools.Util;
using RecipeFinder.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace RecipeFinder.WebAPI.Controllers
{
    public class UserController : ApiBaseController
    {
        private IUserService UserService;

        public UserController()
        {
            UserService = new UserService();
        }

        [HttpPost]
        public async Task<HttpResponseMessage> ValidLogin([FromBody] UserDTO user)
        {
            await Task.Delay(1000);
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email: " + user.Email);
        }

        [HttpPost]
        public HttpResponseMessage ValidLogin123()
        {
            string username = HttpContext.Current.Request.Form["username"].ToString();
            string password = HttpContext.Current.Request.Form["password"].ToString();

            //return Request.CreateErrorResponse(HttpStatusCode.OK, String.Format("{0} {1}", username, password));

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return Request.CreateErrorResponse(HttpStatusCode.OK, "Please fill both username and password");
            }

            if (UserService.ValidLogin(username, password).Result)
            {
                return Request.CreateResponse(HttpStatusCode.OK, TokenManager.GenerateToken(username));
            }

            return Request.CreateErrorResponse(HttpStatusCode.OK, "Username or password is invalid");
        }

        [HttpGet]
        public HttpResponseMessage SomeDatabaseTest()
        {
            try
            {
                UserService.Delete(new UserDTO() { Id = 1 });

                return Request.CreateErrorResponse(HttpStatusCode.OK, "User successfully deleted");

                //return Request.CreateResponse(HttpStatusCode.OK, { success = true, message = "some message"});

            }
            catch (UserValidationException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something horrobly went wrong");
            }
        }

        [HttpGet]
        [RecipeFinderAuthenticationFilter]
        public HttpResponseMessage SecretArea()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "You've got access to the secret area cause you've sent the right auth token in the HTTP header. Authenticated Success: " + IsAuthenticated().ToString() + " Authenticated email: " + AuthenticatedUser().Email);
        }
    }
}