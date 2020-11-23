using RecipeFinder.BusinessLayer.Interfaces;
using RecipeFinder.BusinessLayer.Services;
using RecipeFinder.DTO;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
        public HttpResponseMessage ValidLogin()
        {
            string username = HttpContext.Current.Request.Form["username"].ToString();
            string password = HttpContext.Current.Request.Form["password"].ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return Request.CreateErrorResponse(HttpStatusCode.OK, "Please fill both username and password");
            }

            if (UserService.ValidLogin(username, password))
            {
                return Request.CreateResponse(HttpStatusCode.OK, TokenManager.GenerateToken(username));
            }

            return Request.CreateErrorResponse(HttpStatusCode.OK, "Username or password is invalid");
        }

        [HttpPost]
        [RecipeFinderAuthenticationFilter]
        public HttpResponseMessage SecretArea()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "You've got access to the secret area cause you've sent the right auth token in the HTTP header. Authenticated Success: " + IsAuthenticated().ToString() + " Authenticated email: " + AuthenticatedUser().Email); ;
        }

        public void CreateUser(UserDTO user)
        {
            UserService.Create(user);
        }

        public UserDTO GetUser(int id)
        {
            //UserDTO obj = new UserDTO();
            //obj.Id = 3;
            //obj.Username = "Rasmus - Update";
            //obj.Email = "UpdateRasmus@Mail.dk";
            //obj.Password = "4321";
            //obj.IsAdmin = true;

            //UserService.Update(obj);
            return UserService.Get(id);
        }
        public List<UserDTO> GetAll()
        {
            return UserService.GetAll();
        }
        public void UpdateUser(UserDTO user)
        {
            UserService.Update(user);
        }
        public void DeleteUser(UserDTO user)
        {
            UserService.Delete(user);
        }
    }
}
