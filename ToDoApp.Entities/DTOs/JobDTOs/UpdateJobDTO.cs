using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ToDoApp.Entities.Models;

namespace ToDoApp.Entities.DTOs.JobDTOs
{
    public class UpdateJobDTO
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(256)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(1024)]
        public string Description { get; set; } = string.Empty;
        public DateTime FinalDate { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
    }
}
