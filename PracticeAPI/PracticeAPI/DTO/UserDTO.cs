﻿using System.ComponentModel.DataAnnotations;

namespace PracticeAPI.DTO
{
    public class UserDTO
    {
        public string? UserName { get; set; }

        public string? UserEmail { get; set; }
 
        public string? Password { get; set; }

        public int? IsAdmin { get; set; } = 0;
    }
}
