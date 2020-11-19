using RecipeFinder.BusinessLayer.Interfaces;
using RecipeFinder.BusinessLayer.Services;
using RecipeFinder.DTO;
using System.Collections.Generic;
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
