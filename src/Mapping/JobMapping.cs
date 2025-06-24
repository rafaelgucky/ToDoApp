using ToDoApp.DTOs.JobDTOs;
using ToDoApp.Models;

namespace ToDoApp.Mapping
{
    public class JobMapping
    {
        // Job to DTO

        public ReadJobDTO ToReadJobDTO(Job job)
        {
            return new ReadJobDTO
            {
                Id = job.Id,
                Name = job.Name,
                Description = job.Description,
                Created = job.Created,
                FinalDate = job.FinalDate,
                ImageName = job.ImageName,
                ImageUrl = job.ImageUrl,
                CategoryId = job.CategoryId,
                UserId = job.UserId
            };
        }

        public IEnumerable<ReadJobDTO> ToReadJobDTO(IEnumerable<Job> jobs)
        {
            List<ReadJobDTO> dtos = new List<ReadJobDTO>();
            foreach (Job job in jobs)
            {
                dtos.Add(new ReadJobDTO
                {
                    Id = job.Id,
                    Name = job.Name,
                    Description = job.Description,
                    Created = job.Created,
                    FinalDate = job.FinalDate,
                    ImageName = job.ImageName,
                    ImageUrl = job.ImageUrl,
                    CategoryId = job.CategoryId,
                    UserId = job.UserId
                });
            }
            return dtos;
        }
        public UpdateJobDTO ToUpdateJobDTO(Job job)
        {
            return new UpdateJobDTO
            {
                Id = job.Id,
                Name = job.Name,
                Description = job.Description,
                CategoryId = job.CategoryId,
                FinalDate = job.FinalDate
            };
        }

        // DTO para Job

        public Job ToJob(CreateJobDTO createDto)
        {
            return new Job
            {
                Name = createDto.Name,
                Description = createDto.Description,
                Created = createDto.Created,
                FinalDate = createDto.FinalDate,
                UserId = createDto.UserId,
                CategoryId = createDto.CategoryId
            };
        }

        public Job ToJob(UpdateJobDTO updateDto)
        {
            return new Job
            {
                Id = updateDto.Id,
                Name = updateDto.Name,
                Description = updateDto.Description,
                FinalDate = updateDto.FinalDate,
                CategoryId = updateDto.CategoryId
            };
        }
    }
}
