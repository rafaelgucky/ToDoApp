using System.ComponentModel.DataAnnotations;

namespace ToDoApp.DTOs.JobDTOs
{
    public class UpdateImageJobDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public IFormFile? Image {  get; set; }
    }
}
