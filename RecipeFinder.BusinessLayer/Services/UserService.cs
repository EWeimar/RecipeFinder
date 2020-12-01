using RecipeFinder.BusinessLayer.Interfaces;
using RecipeFinder.BusinessLayer.Lib;
using RecipeFinder.DataLayer;
using RecipeFinder.DataLayer.Models;
using RecipeFinder.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace RecipeFinder.BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork dbAccess;
        public UserService()
        {
            dbAccess = new UnitOfWork();
        }

        public async Task<User> AddAsync(UserDTO userDTO)
        {
            //Convert UserDTO to User
            User user = new User();
            user.Id = 0;
            user.Username = userDTO.Username;
            user.Email = userDTO.Email;
            user.Password = SecurePasswordHasher.Hash(userDTO.Password);
            user.IsAdmin = userDTO.IsAdmin;
            user.CreatedAt = DateTime.Now;

            return await dbAccess.Users.AddAsync(user);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await dbAccess.Users.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await dbAccess.Users.GetAllAsync();
        }

        public async Task<int> UpdateAsync(UserDTO updatedUser)
        {
            var user = await dbAccess.Users.GetByIdAsync(updatedUser.Id);

            //Update the user properties
            user.Username = user.Username;
            user.Email = user.Email;
            user.Password = user.Password;
            user.IsAdmin = user.IsAdmin;

            //Update the user
            return await dbAccess.Users.UpdateAsync(user);
        }

        public async Task<int> DeleteAsync(UserDTO user)
        {
            //Delete the user
            return await dbAccess.Users.DeleteAsync(user.Id);

        }

        public async Task<bool> ValidLogin(string username, string password)
        {
            bool res = false;

            string hashedPassword = await dbAccess.Users.GetUserHashedPassword(username);

            if (!string.IsNullOrEmpty(hashedPassword))
            {
                if (SecurePasswordHasher.Verify(password, hashedPassword))
                {
                    res = true;
                }
            }

            return res;
        }

        public async Task<IEnumerable<User>> FindByCondition(string propName, object value)
        {
            return await dbAccess.Users.FindByCondition(propName, value);
        }
    }
}
