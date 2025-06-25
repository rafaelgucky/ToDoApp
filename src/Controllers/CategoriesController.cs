using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.DTOs.CategoryDTOs;
using ToDoApp.Mapping;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private CategoryServices _services;
        private CategoryMapping _mapping;
        
        public CategoriesController(CategoryServices services, CategoryMapping mapping)
        {
            _services = services;
            _mapping = mapping;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadCategoryDTO>>> FindAllAsync()
        {
            return Ok(_mapping.ToReadCategoryDto(await _services.FindAllAsync<Category>()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadCategoryDTO?>> FindByIdAsync(int id)
        {
            Category? category = await _services.FindByIdAsync(id);
            if (category == null) return BadRequest("Entity not found");
            return Ok(_mapping.ToReadCategoryDto(category));
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateCategoryDTO create)
        {
            if (create == null) return BadRequest("Entity was null");
            Category category = _mapping.ToCategory(create);
            bool response = await _services.CreateAsync(category);
            if (!response) return BadRequest("Not created");
            return Created("api/categories", _mapping.ToReadCategoryDto(category));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync(UpdateCategoryDTO update)
        {
            if (update == null) return BadRequest("Entity was null");
            Category? response = await _services.UpdateAsync(_mapping.ToCategory(update));
            if (response == null) return BadRequest("Not updated");
            return Ok(_mapping.ToReadCategoryDto(response));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            Category? category = await _services.FindByIdAsync(id);
            if (category == null) return BadRequest("Entity not found");
            bool response = await _services.DeleteAsync(category);
            if (!response) return BadRequest("Entity not deleted");
            return Ok("Entity deleted");
        }
    }
}
