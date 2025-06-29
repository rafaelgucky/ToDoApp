using System.ComponentModel.DataAnnotations;
using ToDoApp.Models;

namespace ToDoApp.DTOs.UserDTOs
{
    public class UpdateUserImageDTO
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        [Required, DataType(DataType.Upload)]
        public IFormFile? Image {  get; set; }
    }
}
