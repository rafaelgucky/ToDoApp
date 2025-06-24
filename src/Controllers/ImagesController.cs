using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private ImageServices _imageServices;

        public ImagesController(ImageServices imageServices)
        {
            _imageServices = imageServices;
        }


        [HttpGet("{name}")]
        public async Task<ActionResult> FindFileByNameAsync(string name)
        {
            byte[]? bytes = await _imageServices.FindFileByNameAsync(name);
            if (bytes == null) return NoContent();
            Response.Headers.ContentDisposition = "inline";
            return File(bytes, "application/octet-stream");
        }
    }
}
