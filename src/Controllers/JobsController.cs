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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadJobDTO>>> Index([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            IEnumerable<Job>? jobs = await _services.FindAllAsync<Job>(pageNumber, pageSize);
            if(jobs != null) return Ok(_mapping.ToReadJobDTO(jobs));
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> FindByIdAsync([FromRoute] int id)
        {
            Job? job = await _services.FindByIdAsync(id);
            if (job == null) return BadRequest("Requested data not found");
            return Ok(job);
        }

        [HttpPost]
        public async Task<ActionResult<Job>> Create([FromForm] CreateJobDTO job)
        {
            string imageName = string.Empty;
            List<string> validExtensions = new List<string> 
            { 
                "png", "jpg", "jpeg"
            };

            if(job.Image != null)
            {
                if(!_imageServices.IsValidExtension(job.Image, validExtensions)) return BadRequest("Invalid data type");
                imageName = await _imageServices.SaveImage(job.Image);
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
        public async Task<ActionResult> UpdateImageAsync(UpdateImageJobDTO updateImageDto)
        {
            if (updateImageDto == null) return BadRequest();
            Job? job = await _services.FindByIdAsync(updateImageDto!.Id);
            if (job == null || string.IsNullOrEmpty(job.ImageName)) return BadRequest("Job not found");
            await _imageServices.UpdateAsync(updateImageDto.Image!, job.ImageName!);
            return Ok("Image updated");
        }
    }
}
