using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Interfaces;
using CleanArchitectureTemplate.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;


        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product producto)
        {
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
                throw new ArgumentException("El producto no exite");

            _context.Productos.Remove(producto);

            await _context.SaveChangesAsync();

        }


        public async Task<Product> GetByNameAsync(string name)
        {
            // 1. Validamos que el nombre no venga vacío o nulo
            if (string.IsNullOrWhiteSpace(name))
                return null;

            // 2. Buscamos el producto en la base de datos que coincida con el nombre
            // Usamos EF.Functions.Like si quieres que sea insensible a mayúsculas/minúsculas, 
            // o simplemente un Equals/== estándar de LINQ:
            return await _context.Productos.FirstOrDefaultAsync(p => p.Nombre.ToLower() == name.ToLower());
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            // 1. Validamos que el ID sea un número válido para evitar consultas absurdas
            if (id <= 0)
                return null;

            // 2. Buscamos el producto por su clave primaria
            // FindAsync devuelve automáticamente null si no encuentra el registro
            return await _context.Productos.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Productos.ToListAsync();
        }
    }
}
