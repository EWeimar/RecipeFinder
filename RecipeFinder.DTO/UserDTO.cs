using RecipeFinder.DataLayer.Validators;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.ModelBinding;

namespace RecipeFinder.DTO
{
    public class UserDTO
    { 
        public int Id { get; set; }

        //[Display(Name = "Username")]
        //[Required(ErrorMessage = "Username is required")]
        //[UniqueUsernameValidation(ErrorMessage = "Username already taken!")]
        public string Username { get; set; }

        //[Required]
        //[EmailAddress(ErrorMessage = "Invalid Email")]
        //[UniqueEmailValidation(ErrorMessage = "Email must be unique!")]
        public string Email { get; set; }
        
        //[Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}