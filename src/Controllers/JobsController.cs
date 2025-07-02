using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Data;
using ToDoApp.DTOs.JobDTOs;
using ToDoApp.Mapping;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobsController : ControllerBase
    {
        private Context _context;
        private JobServices _services;
        private JobMapping _mapping;
        private ImageServices _imageServices;
        public JobsController(Context context, JobServices services, JobMapping mapping, ImageServices imageServices) 
        { 
            _context = context;
            _services = services;
            _mapping = mapping;
            _imageServices = imageServices;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<ReadJobDTO>>> FindAllAsync([FromRoute] string userId, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            IEnumerable<Job>? jobs = await _services.FindAllAsync(userId, pageNumber, pageSize);
            if(jobs != null) return Ok(_mapping.ToReadJobDTO(jobs));
            return Ok();
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<ReadJobDTO>> FindByIdAsync([FromRoute] int id)
        {
            Job? job = await _services.FindByIdAsync(id);
            if (job == null) return BadRequest("Requested data not found");
            return Ok(_mapping.ToReadJobDTO(job));
        }

        [HttpPost]
        public async Task<ActionResult<Job>> CreateAsync([FromForm] CreateJobDTO job)
        {
            string? imageName = string.Empty;

            if(job.Image != null)
            {
                imageName = await _imageServices.SaveImage(job.Image, typeof(Job));
                if (imageName == null) return BadRequest("Invalid data type");
            }

            Job saveJob = _mapping.ToJob(job);
            saveJob.ImageName = imageName;
            saveJob.ImageUrl = $"https://localhost:7010/api/Images/{imageName}";

            if (!await _services.CreateAsync(saveJob))
            {
                return BadRequest("Not created");
            }
            return Created("api/jobs", saveJob);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync(UpdateJobDTO update)
        {
            Job? job = await _services.UpdateAsync(_mapping.ToJob(update));
            if (job == null) return BadRequest("Not updated");
            return Ok(_mapping.ToReadJobDTO(job));
        }

        [HttpPut("image")]
        public async Task<ActionResult> UpdateImageAsync(UpdateImageJobDTO updateImageDto)
        {
            if (updateImageDto == null) return BadRequest();
            Job? job = await _services.FindByIdAsync(updateImageDto!.Id);
            if (job == null) return BadRequest("Job not found");
            if(!string.IsNullOrEmpty(job.ImageName))
            {
                await _imageServices.UpdateAsync(updateImageDto.Image!, job.ImageName!);
            }
            else
            {
                await _imageServices.SaveImage(updateImageDto.Image!, typeof(Job));
            }
            return Ok("Image updated");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            Job? job = await _services.FindByIdAsync(id);
            if(job == null) return BadRequest("Entity not found");
            if(job.ImageName != null)
            {
                if(!_imageServices.Remove(job.ImageName, typeof(Job))) return BadRequest("Not deleted (ImageError)");
            }
            bool result = await _services.DeleteAsync(job);
            if (!result) return BadRequest("Entity not deleted");
            return Ok("Entity deleted");

        }
    }
}
