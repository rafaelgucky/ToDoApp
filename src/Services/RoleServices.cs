using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Migrations;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class RoleServices : GenericServices
    {
        public RoleServices(Context context, IConfiguration configuration) : base(context, configuration)
        {
        }

        public async Task<IEnumerable<Role>> GetUserRoles(string userId)
        {
            IEnumerable<UserRoles> userRoles = _context.UserRoles.Where(x => x.UserId == userId);
            List<Role> roles = new List<Role>();
            foreach(UserRoles ur in userRoles)
            {
                Role? role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == ur.RoleId);
                if (role == null) continue;
                roles.Add(role);
            }
            return roles;
        }
    }
}
