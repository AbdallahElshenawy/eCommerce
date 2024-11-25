

namespace eCommerce.Domain.Interfaces
{
    public interface IGeneric<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<int> AddAsync(T entity); 
        Task<int> UpdateAsync(T entity); 
        Task<int> DeleteAsync(Guid id); 
    }
}
