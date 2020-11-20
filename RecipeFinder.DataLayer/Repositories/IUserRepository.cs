using RecipeFinder.DataLayer.Models;
using System.Collections.Generic;

namespace RecipeFinder.DataLayer.Repositories
{
    public interface IUserRepository<User> : IRepository<User>
    {
        bool ValidLogin(string username, string password);
    }
}
