using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(256)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(1024)]
        public string Description { get; set; } = string.Empty;

        [Required, DataType(DataType.DateTime)]
        public DateTime Created {  get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime FinalDate {  get; set; }

        [Required, StringLength(512)]
        public string? ImageUrl {  get; set; }

        [Required, StringLength(256)]
        public string? ImageName { get; set; }
        public User? User { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; } = string.Empty;
        public Category? Category { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
    }
}
