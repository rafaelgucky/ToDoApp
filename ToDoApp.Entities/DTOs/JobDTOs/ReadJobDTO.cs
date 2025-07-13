using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ToDoApp.Entities.Models;

namespace ToDoApp.Entities.DTOs.JobDTOs
{
    public class ReadJobDTO
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(256)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(1024)]
        public string Description { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime FinalDate { get; set; }

        [Required, StringLength(512)]
        public string? ImageUrl { get; set; }

        [Required, StringLength(256)]
        public string? ImageName { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
    }
}
