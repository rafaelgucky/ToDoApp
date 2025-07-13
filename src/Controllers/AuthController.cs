using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using ToDoApp.DTOs.TokenDTO;
using ToDoApp.DTOs.UserDTOs;
using ToDoApp.Mapping;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserServices _userServices;
        private readonly UserMapping _mapping;
        private readonly ImageServices _imageServices;
        private readonly TokenServices _tokenServices;
        private readonly RoleServices _roleServices;
        private readonly IConfiguration _configuration;

        public AuthController(UserServices userServices, TokenServices tokenServices, 
            RoleServices roleServices, ImageServices imageServices, UserMapping mapping, IConfiguration configuration)
        {
            _userServices = userServices;
            _tokenServices = tokenServices;
            _roleServices = roleServices;
            _imageServices = imageServices;
            _mapping = mapping;
            _configuration = configuration;
        }

        [HttpGet("info")]
        public ActionResult Info()
        {
            return Ok(new
            {
                Request.Host,
                Request.Path
            });
        }

        [HttpPost("createaccount")]
        public async Task<ActionResult> CreateAccount(CreateUserDTO create)
        {
            User user = _mapping.ToUser(create);
            user.Id = Guid.NewGuid().ToString();
            string? imageName = string.Empty;

            if (create.Image != null)
            {
                imageName = await _imageServices.SaveImage(create.Image, typeof(User));
                if (imageName == null) return BadRequest("Invalid data type");
                user.ImageName = imageName;
                user.ImageUrl = $"https://{Request.Host}/api/Images/{imageName}";
            }

            User response = await _userServices.CreateAsync(user);
            return Created("api/auth", response);
        }

        [HttpPost("login")]
        public async Task<ActionResult> CreateToken(LoginDTO login)
        {
            User? user = await _userServices.FindByEmailAsync(login.Email);
            if (user == null) return BadRequest("User not found");
            var roles = await _roleServices.GetUserRoles(user.Id);
            user.Roles = roles.ToList();
            if (!_userServices.CkeckPassword(user, login.Password)) return BadRequest("Wrong password");
            return Ok(new
            {
                Token = _tokenServices.CreateToken(user),
                UserId = user.Id,
                Valid = DateTime.Now.AddMinutes(_configuration.GetSection("JWT").GetValue<int>("Expires"))
            });
        }

        [HttpPost("logout")]
        public async Task<ActionResult> LogoutAsync()
        {
            bool result = await _tokenServices.Logout(Request.Headers.Authorization.ToString());
            if (!result) return BadRequest("Error");
            return Ok();
        }
    }
}
