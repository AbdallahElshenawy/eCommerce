using ECommerce.Core.Interfaces;
using ECommerce.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data
{
    public class Repository<T>(ECommerceDbContext context) : IRepository<T> where T:class
    {
        public async Task  <List<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
                }

        public async Task<T> GetById(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await AppySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllSpec(ISpecification<T> spec)
        {
            return await AppySpecification(spec).ToListAsync();
        }

        private IQueryable<T> AppySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(), spec);
        }
    }
}
