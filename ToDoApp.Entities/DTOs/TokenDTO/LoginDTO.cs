﻿using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Entities.DTOs.TokenDTO
{
    public class LoginDTO
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
