using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Entities.DTOs.UserDTOs
{
    public class UpdateUserNameDTO
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        [Required, StringLength(256)]
        public string Name { get; set; } = string.Empty;
    }
}
