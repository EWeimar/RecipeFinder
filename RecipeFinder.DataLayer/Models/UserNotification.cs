using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.DataLayer.Models
{
    public class UserNotification
    {
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
