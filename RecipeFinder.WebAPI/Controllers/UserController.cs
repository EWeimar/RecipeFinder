using RecipeFinder.BusinessLayer.Exceptions;
using RecipeFinder.BusinessLayer.Interfaces;
using RecipeFinder.BusinessLayer.Services;
using RecipeFinder.DTO;
using System;
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

        /*[HttpPost]
        public async Task<HttpResponseMessage> ValidLogin([FromBody] UserDTO user)
        {
            await Task.Delay(1000);
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email: " + user.Email);
        }*/

        [HttpPost]
        public async Task<HttpResponseMessage> ValidLogin([FromBody] UserLoginDTO userLoginDTO)
        {
            string username = userLoginDTO.Username;
            string password = userLoginDTO.Password;

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (await UserService.ValidLogin(username, password))
            {
                return Request.CreateResponse(HttpStatusCode.OK, TokenManager.GenerateToken(username));
            }

            return Request.CreateErrorResponse(HttpStatusCode.OK, "Username or password is invalid");
        }

        [HttpGet]
        [RecipeFinderAuthenticationFilter]
        public HttpResponseMessage SecretArea()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "You've got access to the secret area cause you've sent the right auth token in the HTTP header. Authenticated Success: " + IsAuthenticated().ToString() + " Authenticated email: " + AuthenticatedUser().Email);
        }
    }
}