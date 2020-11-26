using RecipeFinder.DataLayer.Models;
using RecipeFinder.DataLayer.Repositories;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        public User AuthenticatedUser()
        {
            User res = null;

            if (!string.IsNullOrEmpty(RequestContext.Principal.Identity.Name))
            {
                var queryResult = userRepository.GetAll("username", RequestContext.Principal.Identity.Name);

                if (queryResult.Count() == 1)
                {
                    return queryResult.FirstOrDefault();
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
