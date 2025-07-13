using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Entities.DTOs.UserDTOs
{
    public class UpdateUserPasswordDTO
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        [Required, StringLength(16)]
        public string NewPassword { get; set; } = string.Empty;

        [Required, StringLength(16)]
        public string LastPassword { get; set; } = string.Empty;
    }
}
