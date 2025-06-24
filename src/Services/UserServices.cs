using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class UserServices : GenericServices
    {
        public UserServices(Context context) : base(context) { }

        public async Task<User?> FindByIdAsync(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(j => j.Id.Equals(id));
        }
    }
}
