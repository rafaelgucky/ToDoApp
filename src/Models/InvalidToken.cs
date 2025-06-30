using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models
{
    public class InvalidToken
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Token { get; set; } = string.Empty;
    }
}
