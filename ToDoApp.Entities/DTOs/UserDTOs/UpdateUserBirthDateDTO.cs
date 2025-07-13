using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Entities.DTOs.UserDTOs
{
    public class UpdateUserBirthDateDTO
    {
        [Key]
        public string Id { get; set; } = string.Empty;

        [Required, DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }
    }
}
