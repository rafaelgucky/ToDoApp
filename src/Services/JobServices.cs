using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class JobServices : GenericServices
    {
        public JobServices(Context context, IConfiguration configuration) : base(context, configuration) { }

        public async Task<Job?> FindByIdAsync(int id)
        {
            return await _context.Jobs.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
