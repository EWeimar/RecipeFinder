using RecipeFinder.DataLayer.Models;
using RecipeFinder.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.BusinessLayer.Interfaces
{
    public interface IUserService
    {
        Task<User> AddAsync(UserDTO userDTO);
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<IEnumerable<User>> FindByCondition(string propName, object value);
        Task<int> UpdateAsync(UserDTO userDTO);
        Task<int> DeleteAsync(UserDTO userDTO);
        Task<bool> ValidLogin(string strUsername, string strPassword);
    }
}
