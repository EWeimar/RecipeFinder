using RecipeFinder.BusinessLayer.Interfaces;
using RecipeFinder.DataLayer;
using RecipeFinder.DataLayer.Models;
using RecipeFinder.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork dbAccess;
        public UserService()
        {
            dbAccess = new UnitOfWork();
        }
        public void Create(UserDTO user)
        {
            //Create User in the DB
            User u = new User();
            u.Id = 0;
            u.Username = user.Username;
            u.Email = user.Email;
            u.Password = user.Password;
            u.IsAdmin = user.IsAdmin;
            u.CreatedAt = DateTime.Now;

            var newUser = dbAccess.Users.Create(u);
        }

        public UserDTO Get(int id)
        {
            var getUser = dbAccess.Users.Get(id);
            //Check if user exists
            if(getUser == null)
            {
                throw new ArgumentNullException("The user could not be found!");
            }

            UserDTO result = new UserDTO();
            result.Id = getUser.Id;
            result.Username = getUser.Username;
            result.Email = getUser.Email;
            result.Password = getUser.Password;
            result.IsAdmin = getUser.IsAdmin;
            result.CreatedAt = getUser.CreatedAt;

            return result;
        }

        public List<UserDTO> GetAll()
        {
            //Create list for users
            List<UserDTO> userList = new List<UserDTO>();
            var getAll = dbAccess.Users.GetAll();

            //Check for any users
            if(getAll == null)
            {
                throw new ArgumentNullException("No users were found!");
            }

            //Loop over users in DB, convert to DTO and add to userList
            foreach (var item in getAll)
            {
                var user = dbAccess.Users.Get(item.Id);

                UserDTO uResult = new UserDTO();
                uResult.Id = user.Id;
                uResult.Username = user.Username;
                uResult.Email = user.Email;
                uResult.Password = user.Password;
                uResult.IsAdmin = user.IsAdmin;
                uResult.CreatedAt = user.CreatedAt;

                userList.Add(uResult);
            }

            return userList;
        }

        public void Update(UserDTO user)
        {
            var updateUser = dbAccess.Users.Get(user.Id);

            //Check if user exists
            if(updateUser == null)
            {
                throw new ArgumentNullException("The user could not be found!");
            }

            //Update the user properties
            updateUser.Username = user.Username;
            updateUser.Email = user.Email;
            updateUser.Password = user.Password;
            updateUser.IsAdmin = user.IsAdmin;

            //Update the user
            dbAccess.Users.Update(updateUser);
        }
        public void Delete(UserDTO user)
        {
            var deleteUser = dbAccess.Users.Get(user.Id);
            //Check if user exists
            if(deleteUser == null)
            {
                throw new ArgumentNullException("The user could not be found!");
            }

            //Delete the user
            dbAccess.Users.Delete(deleteUser.Id);

            //Check if the user has been deleted
            if(dbAccess.Users.Get(deleteUser.Id) == null)
            {
                Console.WriteLine("User has been deleted");
            }
        }

        public bool ValidLogin(string username, string password)
        {
            return dbAccess.Users.ValidLogin(username, password);
        }
    }
}
