﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Entities.DTOs.UserDTOs
{
    public class CreateUserDTO
    {

        [Required, StringLength(256)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(512)]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(16)]
        public string Password { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public IFormFile? Image { get; set; }
    }
}
