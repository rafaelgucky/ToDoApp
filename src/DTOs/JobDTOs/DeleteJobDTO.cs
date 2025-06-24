using System.ComponentModel.DataAnnotations;

namespace ToDoApp.DTOs.JobDTOs
{
    public class DeleteJobDTO
    {
        [Key]
        public int Id { get; set; }
    }
}
