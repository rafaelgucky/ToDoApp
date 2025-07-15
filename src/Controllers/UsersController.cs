using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Entities.DTOs.UserDTOs;
using ToDoApp.Entities.Mapping;
using ToDoApp.Entities.Models;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<ReadUserDTO>>> FindAllAsync()
        //{
        //    IEnumerable<User>? users = await _services.FindAllAsync<User>();
        //    if (users == null) return Ok();
        //    return Ok(_mapping.ToReadUserDto(users));
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadUserDTO?>> FindByIdAsync(string id)
        {
            User? user = await _services.FindByIdAsync(id);
            if(user == null) return Ok();
            return Ok(_mapping.ToReadUserDto(user));
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
                user.ImageName = imageName;
                user.ImageUrl = $"https://{Request.Host}/api/Images/{imageName}";
            }

            User response = await _services.CreateAsync(user);
            return Created("api/users", response);
        }

        [HttpPut("name")]
        public async Task<ActionResult> UpdateNameAsync(UpdateUserNameDTO update)
        {
            if(!await _services.UpdateUserNameAsync(update))
            {
                return BadRequest("User name not uodated");
            }
            return Ok($"New user name: {update.Name}");
        }

        [HttpPut("password")]
        public async Task<ActionResult> UpdatePasswordAsync(UpdateUserPasswordDTO update)
        {
            User? user = await _services.FindByIdAsync(update.Id);
            if (user == null) return BadRequest("User not found");
            if (!_services.CkeckPassword(user, update.LastPassword)) return BadRequest("The passwords aren't the same");
            if(!await _services.UpdateUserPasswordAsync(update)) return BadRequest("Not updated");
            return Ok("Updated");
        }

        [HttpPut("birthdate")]
        public async Task<ActionResult> UpdateBirthDateAsync(UpdateUserBirthDateDTO update)
        {
            bool response = await _services.UpdateUserBirthDateAsync(update);
            if (!response) return BadRequest("Not updated");
            return Ok("Updated");
        }

        [HttpPut("image")]
        public async Task<ActionResult> UpdateImageAsync(UpdateUserImageDTO update)
        {
            if (update.Image == null) return BadRequest("File can't be null");
            User? user = await _services.FindByIdAsync(update.Id);
            if (user == null) return BadRequest("User not found");
            if (!string.IsNullOrEmpty(user.ImageName))
            {
                await _imageServices.UpdateAsync(update.Image!, user.ImageName!);
            }
            else
            {
                await _imageServices.SaveImage(update.Image, typeof(User));
            }
                return Ok("Image updated");
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(DeleteUserDTO delete)
        {
            User? user = await _services.FindByIdAsync(delete.Id);
            if (user == null) return BadRequest("User not found");
            if (!_services.CkeckPassword(user, delete.Password)) return BadRequest("Wrong password");
            if (!string.IsNullOrEmpty(user.ImageName))
            {
                if(!_imageServices.Remove(user.ImageName, typeof(User))) return BadRequest("Not deleted (ImageError)");
            }
            if (!await _services.DeleteAsync(user)) return BadRequest("Not deleted");
            return Ok($"User with Id: {delete.Id} was deleted");
        }
    }
}
