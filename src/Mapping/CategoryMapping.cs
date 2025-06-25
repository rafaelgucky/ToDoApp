using System.Security.Cryptography.X509Certificates;
using ToDoApp.DTOs.CategoryDTOs;
using ToDoApp.Models;

namespace ToDoApp.Mapping
{
    public class CategoryMapping
    {
        // Category to DTO

        public ReadCategoryDTO ToReadCategoryDto(Category category)
        {
            return new ReadCategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                HexadecimalColor = category.HexadecimalColor
            };
        }

        public IEnumerable<ReadCategoryDTO>ToReadCategoryDto(IEnumerable<Category>? categories)
        {
            List<ReadCategoryDTO> dtos = new List<ReadCategoryDTO>();
            foreach (var category in categories)
            {
                dtos.Add(new ReadCategoryDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    HexadecimalColor = category.HexadecimalColor
                });
            }
            return dtos;           
        }

        // DTO to Category

        public Category ToCategory(CreateCategoryDTO create)
        {
            return new Category
            {
                Name = create.Name,
                Description = create.Description
            };
        }

        public Category ToCategory(UpdateCategoryDTO update)
        {
            return new Category
            {
                Id = update.Id,
                Name = update.Name,
                Description = update.Description
            };
        }

        public UpdateCategoryDTO ToUpdateCategoryDto(Category category)
        {
            return new UpdateCategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }
    }
}
