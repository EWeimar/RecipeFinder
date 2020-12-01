using System;
using System.ComponentModel.DataAnnotations;

namespace RecipeFinder.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}