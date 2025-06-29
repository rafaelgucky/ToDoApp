using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using ToDoApp.DTOs.TokenDTO;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserServices _userServices;
        private readonly TokenServices _tokenServices;
        private readonly RoleServices _roleServices;

        public AuthController(UserServices userServices, TokenServices tokenServices, RoleServices roleServices)
        {
            _userServices = userServices;
            _tokenServices = tokenServices;
            _roleServices = roleServices;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> CreateToken(LoginDTO login)
        {
            User? user = await _userServices.FindByEmailAsync(login.Email);
            if (user == null) return BadRequest("User not found");
            var roles = await _roleServices.GetUserRoles(user.Id);
            user.Roles = roles.ToList();
            if (!_userServices.CkeckPassword(user, login.Password)) return BadRequest("Wrong password");
            return Ok(_tokenServices.CreateToken(user));
        }
    }
}
