using RecipeFinder.BusinessLayer.Interfaces;
using RecipeFinder.BusinessLayer.Services;
using RecipeFinder.DTO;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecipeFinder.WebAPI.Controllers
{
    public class UserController : ApiController
    {
        private IUserService UserService;

        public UserController()
        {
            UserService = new UserService();
        }

        [HttpGet]
        public HttpResponseMessage ValidLogin(string username, string password)
        {
            if (username == "admin" && password == "123456") // skal checkes i databasen
            {
                return Request.CreateResponse(HttpStatusCode.OK, TokenManager.GenerateToken(username));
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadGateway, "Username or password is invalid");
            }
        }

        [HttpGet]
        [RecipeFinderAuthenticationFilter]
        public HttpResponseMessage SecretArea()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "You've got access to the secret area!");
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
