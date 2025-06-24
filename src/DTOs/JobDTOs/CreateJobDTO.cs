using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ToDoApp.Models;

namespace ToDoApp.DTOs.JobDTOs
{
    public class CreateJobDTO
    {
        [Required, StringLength(256)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(1024)]
        public string Description { get; set; } = string.Empty;

        [Required, DataType(DataType.DateTime)]
        public DateTime FinalDate { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        public IFormFile? Image { get; set; }


        [ForeignKey("UserId")]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
    }
}
