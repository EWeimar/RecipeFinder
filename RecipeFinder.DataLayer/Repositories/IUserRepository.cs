﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.DataLayer.Repositories
{
    public interface IUserRepository<User> : IRepository<User>
    {
        Task<string> GetUserHashedPassword(string username);
    }
}
