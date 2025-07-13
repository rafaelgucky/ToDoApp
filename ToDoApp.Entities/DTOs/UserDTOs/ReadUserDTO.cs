using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Entities.DTOs.UserDTOs
{
    public class ReadUserDTO
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        [Required, StringLength(256)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(512)]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(16)]
        public string Password { get; set; } = string.Empty;

        [Required, StringLength(512)]
        public string? ImageUrl { get; set; }

        [Required, StringLength(256)]
        public string? ImageName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age => (int)Math.Floor(DateTime.Now.Subtract(BirthDate).Days / 365.25);

    }
}
