﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(256)]
        public string Name { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }
    }
}
