using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.Entities.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        
        [Required, StringLength(256)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(1024)]
        public string Description { get; set; } = string.Empty;

        [StringLength(50)]
        public string? HexadecimalColor { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; } = string.Empty;

        public User? User { get; set; }

        public List<Job>? Jobs { get; set; }

    }
}
