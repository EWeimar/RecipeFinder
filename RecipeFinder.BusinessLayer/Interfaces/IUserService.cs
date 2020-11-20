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
        void Create(UserDTO user);
        UserDTO Get(int id);
        List<UserDTO> GetAll();
        void Update(UserDTO user);
        void Delete(UserDTO user);
        bool ValidLogin(string strUsername, string strPassword);
    }
}
