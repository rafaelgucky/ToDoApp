using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        
        [Required, StringLength(256)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(1024)]
        public string Description { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string HexadecimalColor { get; set; } = string.Empty;

        public List<Job>? Jobs { get; set; }

    }
}
