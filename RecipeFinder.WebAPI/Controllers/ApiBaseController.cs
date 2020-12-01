using RecipeFinder.DataLayer.Models;
using RecipeFinder.DataLayer.Repositories;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RecipeFinder.WebAPI.Controllers
{
    public class ApiBaseController : ApiController
    {
        private UserRepository userRepository;

        public ApiBaseController()
        {
            userRepository = new UserRepository(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        }

        public async Task<User> AuthenticatedUser()
        {
            User res = null;

            if (!string.IsNullOrEmpty(RequestContext.Principal.Identity.Name))
            {
                var userExists = await userRepository.FindByCondition("username", RequestContext.Principal.Identity.Name);

                User user = userExists.Single();

                if (user != null)
                {
                    return user;
                }
            }

            return res;
        }

        public bool IsAuthenticated()
        {
            return !string.IsNullOrEmpty(RequestContext.Principal.Identity.Name) ? true : false;
        }

    }
}
