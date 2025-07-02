using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel;
using ToDoApp.Data;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class CategoryServices : GenericServices
    {
        public CategoryServices(Context context, IConfiguration configuration) : base(context, configuration) { }

        public async Task<bool> CreateAsync(Category category)
        {
            string color = await GenerateColor();
            category.HexadecimalColor = color;
            _context.Categories.Add(category);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<Category>> FindAllAsync(string userId, int pageNumber, int pageSize)
        {
            return await _context.Categories
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
        public async Task<Category?> FindByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            EntityEntry response = _context.Categories.Update(category);
            _context.Entry(category).Property(c => c.HexadecimalColor).IsModified = false;
            _context.Entry(category).Property(c => c.UserId).IsModified = false;
            await _context.SaveChangesAsync();
            return category;
        }

        private async Task<string> GenerateColor()
        {
            List<char> hex = new List<char> { 'A', 'B', 'C', 'D', 'F' };
            List<int> ints = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            string result = string.Empty;
            do
            {
                result = "#";
                for (int i = 0; i < 6; i++)
                {
                    int num = new Random().Next(0, 2);
                    if (num == 0)
                    {
                        int listNum = new Random().Next(0, 5);
                        result += hex[listNum];
                    }
                    else
                    {
                        int listNum = new Random().Next(0, 10);
                        result += ints[listNum];
                    }
                }
            }
            while(await _context.Categories.AnyAsync(c => c.HexadecimalColor == result));
            return result;
        }
    }
}
