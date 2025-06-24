using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class CategoryServices : GenericServices
    {
        public CategoryServices(Context context) : base(context) { }

        public async Task<Category?> FindByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
