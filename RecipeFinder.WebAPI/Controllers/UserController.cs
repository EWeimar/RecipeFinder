using RecipeFinder.BusinessLayer.Exceptions;
using RecipeFinder.BusinessLayer.Interfaces;
using RecipeFinder.BusinessLayer.Services;
using RecipeFinder.DTO;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;

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
        [Route("api/user/create")]
        public async Task<HttpResponseMessage> Create([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Empty;

                foreach (ModelState keyValuePairs in ModelState.Values)
                {
                    foreach (ModelError modelError in keyValuePairs.Errors)
                    {
                        errorMessage += " - " + modelError.ErrorMessage;
                    }
                }

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
            }

            if (await UserService.AddAsync(userDTO) != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { message = "User was succesfully created" });
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError, new { message = "Something horribly went wrong." });
        }

        [HttpPost]
        [Route("api/user/login")]
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

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Username or password is invalid");
        }

        [HttpGet]
        [RecipeFinderAuthenticationFilter]
        [Route("api/user/secret-area")]
        public async Task<HttpResponseMessage> SecretArea()
        {
            var au = await AuthenticatedUser(); // how to get current auth user

            return Request.CreateResponse(HttpStatusCode.OK, "You've got access to the secret area cause you've sent the right auth token in the HTTP header. Authenticated Success: " + IsAuthenticated().ToString() + " Authenticated email: " + au.Email);
        }

        [HttpGet]
        [Route("api/user/tester321")]
        public async Task<HttpResponseMessage> ALongNameButTestingRoutes()
        {
            Task.Delay(100);
            return Request.CreateResponse(HttpStatusCode.OK, "Hey It Works!");
        }
    }
}