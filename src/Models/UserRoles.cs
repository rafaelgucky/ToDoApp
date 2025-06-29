using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.Models
{
    public class UserRoles
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }

        [ForeignKey("RoleId")]
        public int RoleId { get; set; }
        public Role? Role { get; set; }
    }
}
