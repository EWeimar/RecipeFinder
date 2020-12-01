using RecipeFinder.DataLayer;
using RecipeFinder.DataLayer.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeFinder.DataLayer.Validators
{
    public class UniqueEmailValidation : ValidationAttribute
    {
        private readonly UnitOfWork dbAccess = new UnitOfWork();
        public override bool IsValid(object value)
        {
            bool res = true;

            if(value != null)
            {
                IEnumerable<User> check = Task.Run<IEnumerable<User>>(() => dbAccess.Users.FindByCondition("email", value)).GetAwaiter().GetResult();

                if (check.Any())
                {
                    res = false;
                }
            }

            return res;
        }
    }

}