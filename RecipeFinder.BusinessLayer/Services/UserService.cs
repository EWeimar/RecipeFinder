using RecipeFinder.BusinessLayer.Interfaces;
using RecipeFinder.BusinessLayer.Lib;
using RecipeFinder.DataLayer;
using RecipeFinder.DataLayer.Models;
using RecipeFinder.DTO;
using System;
using System.Collections.Generic;

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
            u.Password = SecurePasswordHasher.Hash(user.Password);
            u.IsAdmin = user.IsAdmin;
            u.CreatedAt = DateTime.Now;

            var newUser = dbAccess.Users.AddAsync(u);
        }

        public UserDTO Get(int id)
        {
            var getUser = dbAccess.Users.GetByIdAsync(id).Result;
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
            var getAll = dbAccess.Users.GetAllAsync().Result;

            //Check for any users
            if(getAll == null)
            {
                throw new ArgumentNullException("No users were found!");
            }

            //Loop over users in DB, convert to DTO and add to userList
            foreach (var item in getAll)
            {
                var user = dbAccess.Users.GetByIdAsync(item.Id).Result;

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

        public void Update(UserDTO updatedUser)
        {
            var user = dbAccess.Users.GetByIdAsync(updatedUser.Id).Result;

            //Check if user exists
            if(user == null)
            {
                throw new ArgumentNullException("The user could not be found!");
            }

            //Update the user properties
            user.Username = user.Username;
            user.Email = user.Email;
            user.Password = user.Password;
            user.IsAdmin = user.IsAdmin;

            //Update the user
            dbAccess.Users.UpdateAsync(user);
        }
        public void Delete(UserDTO user)
        {
            var deleteUser = dbAccess.Users.GetByIdAsync(user.Id);
            //Check if user exists
            if(deleteUser == null)
            {
                throw new ArgumentNullException("The user could not be found!");
            }

            //Delete the user
            dbAccess.Users.DeleteAsync(deleteUser.Id);

            //Check if the user has been deleted
            if(dbAccess.Users.GetByIdAsync(deleteUser.Id) == null)
            {
                Console.WriteLine("User has been deleted");
            }
        }

        public bool ValidLogin(string username, string password)
        {
            bool res = false;

            //string hashedPassword = dbAccess.Users.GetUserHashedPassword(username);
            string hashedPassword = string.Empty;

            if (!string.IsNullOrEmpty(hashedPassword))
            {
                if (SecurePasswordHasher.Verify(password, hashedPassword))
                {
                    res = true;
                }
            }

            return res;
        }
    }
}
