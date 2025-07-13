using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Entities.Models
{
    public class InvalidToken
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Token { get; set; } = string.Empty;
    }
}
