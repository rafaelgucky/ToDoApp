using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Entities.DTOs.UserDTOs
{
    public class DeleteUserDTO
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        [Required, StringLength(16)]
        public string Password { get; set; } = string.Empty;

    }
}
