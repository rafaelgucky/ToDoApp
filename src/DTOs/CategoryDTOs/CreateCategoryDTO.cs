using System.ComponentModel.DataAnnotations;

namespace ToDoApp.DTOs.CategoryDTOs
{
    public class CreateCategoryDTO
    {
        [Required, StringLength(256)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(1024)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; } = string.Empty;
    }
}
