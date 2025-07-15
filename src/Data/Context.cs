using Microsoft.EntityFrameworkCore;
using ToDoApp.Entities.Models;

namespace ToDoApp.Data
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<InvalidToken> InvakidTokens { get; set; }

        public Context(DbContextOptions<Context> options) : base(options) { }
    }
}
