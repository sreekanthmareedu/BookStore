﻿using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Model.DTO
{
    public class UserCreateDTO
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
