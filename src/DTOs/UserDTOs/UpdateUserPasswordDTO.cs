using System.ComponentModel.DataAnnotations;
using ToDoApp.Models;

namespace ToDoApp.DTOs.UserDTOs
{
    public class UpdateUserPasswordDTO
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        [Required, StringLength(16)]
        public string Password { get; set; } = string.Empty;
    }
}
