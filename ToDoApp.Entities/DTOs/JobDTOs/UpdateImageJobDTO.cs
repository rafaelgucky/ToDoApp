using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Entities.DTOs.JobDTOs
{
    public class UpdateImageJobDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public IFormFile? Image { get; set; }
    }
}
