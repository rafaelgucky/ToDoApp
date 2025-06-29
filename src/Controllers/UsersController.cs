using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.DTOs.UserDTOs;
using ToDoApp.Mapping;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserServices _services;
        private readonly ImageServices _imageServices;
        private readonly UserMapping _mapping;

        public UsersController(UserServices services, ImageServices imageServices, UserMapping mapping)
        {
            _services = services;
            _imageServices = imageServices;
            _mapping = mapping;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> FindAllAsync()
        {
            return Ok(await _services.FindAllAsync<User>());
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateUserDTO create)
        {
            User user = _mapping.ToUser(create);
            user.Id = Guid.NewGuid().ToString();
            string? imageName = string.Empty;

            if(create.Image != null)
            {
                imageName = await _imageServices.SaveImage(create.Image, typeof(User));
                if (imageName == null) return BadRequest("Invalid data type");
            }

            user.ImageName = imageName;
            user.ImageUrl = $"https://localhost:5203/api/Images/{imageName}";


            User response = await _services.CreateAsync(user);
            return Created("api/users", response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateImageAsync(UpdateUserImageDTO update)
        {
            if (update.Image == null) return BadRequest("File can't be null");
            User? user = await _services.FindByIdAsync(update.Id);
            if (user == null) return BadRequest("User not found");
            await _imageServices.UpdateAsync(update.Image!, user.ImageName!);
            return Ok("Image updated");
        }
    }
}
