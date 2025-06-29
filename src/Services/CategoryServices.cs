using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ToDoApp.Data;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class CategoryServices : GenericServices
    {
        public CategoryServices(Context context, IConfiguration configuration) : base(context, configuration) { }

        public async Task<Category?> FindByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            EntityEntry response = _context.Categories.Update(category);
            _context.Entry(category).Property(c => c.HexadecimalColor).IsModified = false;
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
