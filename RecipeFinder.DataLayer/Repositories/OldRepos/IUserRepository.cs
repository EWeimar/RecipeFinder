using RecipeFinder.DataLayer.Models;
using System.Collections.Generic;

namespace RecipeFinder.DataLayer.OldRepositories
{
    public interface IUserRepository<User> : IRepository<User>
    {
        string GetUserHashedPassword(string username);
    }
}
