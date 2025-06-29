using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ToDoApp.Data;

namespace ToDoApp.Services
{
    public class GenericServices
    {
        protected Context _context;
        protected IConfiguration _configuration;

        public GenericServices(Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> CreateAsync<T>(T entity) where T : class
        {
            EntityEntry result = _context.Add<T>(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<T>?> FindAllAsync<T>(int pageNumber = 1, int count = 10) where T : class
        {
            return await _context.Set<T>().Skip(pageNumber - 1 * count).Take(count).ToListAsync();
        }

        public async Task<T?> UpdateAsync<T>(T entity) where T : class
        {
            EntityEntry result = _context.Update(entity);
            return await _context.SaveChangesAsync() > 0 ? result.Entity as T : null;
        }

        public async Task<bool> DeleteAsync<T>(T entity) where T : class
        {
            EntityEntry result = _context.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
