using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Entities.DTOs.UserDTOs
{
    public class UpdateUserImageDTO
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        [Required, DataType(DataType.Upload)]
        public IFormFile? Image { get; set; }
    }
}
