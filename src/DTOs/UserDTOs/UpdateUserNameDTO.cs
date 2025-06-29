using System.ComponentModel.DataAnnotations;
using ToDoApp.Models;

namespace ToDoApp.DTOs.UserDTOs
{
    public class UpdateUserNameDTO
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        [Required, StringLength(256)]
        public string Name { get; set; } = string.Empty;
    }
}
