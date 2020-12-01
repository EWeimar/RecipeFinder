using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.DataLayer.Models
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "The username is required")]
        public string Username { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Username")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public bool IsAdmin { get; set; } //Default false
        public DateTime CreatedAt { get; set; }
    }
}
