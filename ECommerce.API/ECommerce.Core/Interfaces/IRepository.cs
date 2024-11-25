using ECommerce.Core.Entities;
using ECommerce.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<List<T>> GetAllSpec(ISpecification<T> spec);
    }
}
