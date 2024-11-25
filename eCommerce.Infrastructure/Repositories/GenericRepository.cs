using eCommerce.Application.Exceptions;
using eCommerce.Domain.Interfaces;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace eCommerce.Infrastructure.Repositories
{
    public class GenericRepository<T>(AppDbContext context) : IGeneric<T> where T : class
    {
        public async Task<int> AddAsync(T entity)
        {
            context.Set<T>().Add(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var entity = await context.Set<T>().FindAsync(id);
            if (entity is null)
                return 0;
            context.Set<T>().Remove(entity);
            return await context.SaveChangesAsync();
            
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await context.Set<T>().FindAsync(id);
            return entity!;
        }

        public async Task<int> UpdateAsync(T entity)
        {
            context.Set<T>().Update(entity);
            return await context.SaveChangesAsync();
        }
    }
}
