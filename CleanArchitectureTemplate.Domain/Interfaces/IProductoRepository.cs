using CleanArchitectureTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync();
        Task<Product> GetByNameAsync(string name);
        Task AddAsync(Product producto);
        Task UpdateAsync(Product producto);
        Task DeleteAsync(int id);
    }
}
