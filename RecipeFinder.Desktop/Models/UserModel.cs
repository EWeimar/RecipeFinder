﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeFinder.Desktop.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}