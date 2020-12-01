using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeFinder.DTO
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "The username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The password is required")]
        public string Password { get; set; }
    }
}